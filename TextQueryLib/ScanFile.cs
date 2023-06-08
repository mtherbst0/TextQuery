using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Kusto.Language.Symbols;
using BabyKusto.Core;
using BabyKusto.Core.Util;
using BabyKusto.Core.Evaluation;
using System.Text.Json.Serialization;

namespace TextQueryLib
{
    public class ScanResult : ITableSource
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        public ScanResult(IDataProcessor processor)
        {
            this.Columns = processor.Columns.Select(col => new ScanColumn(col)).ToList();
            this.Rows = processor.Rows.Select(row => new ScanRow(row.Select((cell, index) => new { Key = this.Columns[index].Title, Value = cell }).ToDictionary(kv => kv.Key, kv => kv.Value))).ToList();
        }

        public ScanResult(ITableSource table)
        {
            this.Columns = table.Type.Columns.Select(c => new ScanColumn(c.Name)).ToList();

            var rows = new List<ScanRow>();
            foreach (var chunk in table.GetData())
            {
                for (int i = 0; i < chunk.RowCount; i++)
                {
                    var dictionary = new Dictionary<string, string>();
                    for (int j = 0; j < table.Type.Columns.Count; j++)
                    {
                        object v = chunk.Columns[j].RawData.GetValue(i)!;
                        dictionary.Add(
                            table.Type.Columns[j].Name,
                            v switch
                            {
                                DateTime dateTime => dateTime.ToString("O"),
                                JsonNode jsonNode => jsonNode.ToJsonString(JsonOptions),
                                null => "(null)",
                                _ => v.ToString()!,
                            });
                    }
                    rows.Add(new ScanRow(dictionary));
                }
            }

            this.Rows = rows;
        }

        public List<ScanColumn> Columns { get; }

        public List<ScanRow> Rows { get; }

        public TableSymbol Type =>
            new TableSymbol(
                "result",
                this.Columns.Select(c => new ColumnSymbol(c.Title, ScalarTypes.String)).ToArray()
            );

        public IEnumerable<ITableChunk> GetData()
        {
            var builders = new ColumnBuilder<string?>[this.Columns.Count];

            for (int i = 0; i < this.Columns.Count; i++)
            {
                builders[i] = new ColumnBuilder<string?>(ScalarTypes.String);
            }

            foreach (var row in this.Rows)
            {
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    builders[i].Add(row.Data[this.Columns[i].Title]);
                }
            }

            yield return new TableChunk(this, builders.Select(b => b.ToColumn()).ToArray());
        }

        public IAsyncEnumerable<ITableChunk> GetDataAsync(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }

    public class ScanColumn
    {
        public ScanColumn(string title)
        {
            this.Title = title;
        }

        public string Title { get; }
    }

    public class ScanRow
    {
        public ScanRow(Dictionary<string, string> data)
        {
            this.Data = data;
        }

        public Dictionary<string, string> Data { get; }
    }

    public interface IDataProcessor
    {
        string[] Columns { get; }

        IEnumerable<string[]> Rows { get; }

        static string[] AddFileNameColumn(AllData data, string[] columns)
            => data.IncludeFileNameColumn ? columns.Prepend("_FileName").ToArray() : columns;

        static IEnumerable<string> AddFileNameToRow(AllData data, string? fileName, IEnumerable<string> row)
            => data.IncludeFileNameColumn ? row.Prepend(fileName) : row;
    }

    public class RegexDataProcessor : IDataProcessor
    {
        public static string[] GetRegexGroupNames(Regex regex, bool includeWholeMatch)
        {
            return regex.GetGroupNames().Where(group => includeWholeMatch || group != "0").ToArray();
        }

        public RegexDataProcessor(string regexExpr, bool includeWholeMatch, bool global, AllData data, IAsyncState asyncState)
        {
            var regex = new Regex(regexExpr, (global ? RegexOptions.None : RegexOptions.Multiline) | RegexOptions.Compiled);
            var regexColumns = GetRegexGroupNames(regex, includeWholeMatch);
            this.Columns = IDataProcessor.AddFileNameColumn(data, regexColumns);
            this.Rows = data.Files.SelectMany((file, index) =>
            {
                if (asyncState.IsCancellationPending())
                {
                    return Enumerable.Empty<string[]>();
                }
                asyncState.ReportStatus(index * 100 / data.Files.Length, $"Loading file: {file.FileName}");
                using (var reader = new StreamReader(file.Stream))
                {
                    var wholeFile = reader.ReadToEnd();
                    var matches = regex.Matches(wholeFile);
                    return matches.Select(match => IDataProcessor.AddFileNameToRow(data, file.FileName, regexColumns.Select(col => match.Groups[col].Value)).ToArray());
                }
            });
        }

        public string[] Columns { get; }

        public IEnumerable<string[]> Rows { get; }
    }

    public class AllData : IDisposable
    {
        public AllData(Stream file)
        {
            this.IncludeFileNameColumn = false;
            this.Files = new[] { new NamedStream(file) };
        }

        public AllData(NamedStream[] files)
        {
            this.IncludeFileNameColumn = true;
            this.Files = files;
        }

        public bool IncludeFileNameColumn { get; }

        public NamedStream[] Files { get; }

        public void Dispose()
        {
            foreach (var file in this.Files)
            {
                file.Dispose();
            }
        }
    }

    public class NamedStream : IDisposable
    {
        public NamedStream(Stream file)
        {
            this.stream = file;
        }

        public NamedStream(string fileName)
        {
            this.FileName = fileName;
        }

        public string? FileName { get; }

        private Stream? stream;

        public Stream Stream
        {
            get
            {
                if (stream == null)
                {
                    if (this.FileName == null)
                    {
                        throw new Exception("Invalid state!");
                    }

                    stream = File.OpenRead(this.FileName);
                }

                return stream;
            }
        }

        public void Dispose()
        {
            this.stream?.Dispose();
        }
    }

    public interface IAsyncState
    {
        bool IsCancellationPending();

        void ReportStatus(int percentage, string message);
    }

    public static class ScanFile
    {
        public static ScanResult QueryScanResult(ScanResult scanResult, string query)
        {
            var engine = new BabyKustoEngine();
            engine.AddGlobalTable(scanResult);
            var kustoResult = engine.Evaluate(query);
            var tabularResult = (TabularResult)kustoResult!;
            return new ScanResult(tabularResult.Value);
        }

        public static ScanResult ProcessWithRegex(string regexExpr, AllData data, bool includeWholeMatch, bool global, IAsyncState asyncState)
        {
            var dataProcessor = new RegexDataProcessor(regexExpr, includeWholeMatch, global, data, asyncState);

            return new ScanResult(dataProcessor);

            ////var globals = GlobalState.Default.WithDatabase(
            ////    new DatabaseSymbol("db",
            ////        new TableSymbol("result", new ColumnSymbol("a", ScalarTypes.String), new ColumnSymbol("b", ScalarTypes.String))));
            ////
            ////var query = "result | project a = strcat(a, b) | where a == \"10.0\"";
            ////var code = KustoCode.ParseAndAnalyze(query, globals);
            ////var diagnostics = code.GetDiagnostics();
        }
    }

    [JsonDerivedType(typeof(FileDataSourceProfile), "file")]
    [JsonDerivedType(typeof(FilesDataSourceProfile), "files")]
    public abstract class DataSourceProfileBase
    {
    }

    public class FileDataSourceProfile : DataSourceProfileBase
    {
        [JsonPropertyName("file")]
        public string? File { get; set; }
    }

    public class FilesDataSourceProfile : DataSourceProfileBase
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("pattern")]
        public string? Pattern { get; set; }

        [JsonPropertyName("recursive")]
        public bool? Recursive { get; set; }
    }

    [JsonDerivedType(typeof(RegexProcessorProfile), "regex")]
    public abstract class ProcessorProfileBase
    {
    }

    public class RegexProcessorProfile : ProcessorProfileBase
    {
        [JsonPropertyName("regex")]
        public string? Regex { get; set; }

        [JsonPropertyName("includeWholeMatch")]
        public bool? IncludeWholeMatch { get; set; }

        [JsonPropertyName("global")]
        public bool? Global { get; set; }
    }

    [JsonDerivedType(typeof(KustoProfile), "kusto")]
    public abstract class QueryProfileBase
    {
    }

    public class KustoProfile : QueryProfileBase
    {
        [JsonPropertyName("query")]
        public string? Query { get; set; }
    }

    public class ScanProfile
    {
        [JsonPropertyName("source")]
        public DataSourceProfileBase? Source { get; set; }

        [JsonPropertyName("processor")]
        public ProcessorProfileBase? Processor { get; set; }

        [JsonPropertyName("query")]
        public QueryProfileBase? Query { get; set; }
    }
}