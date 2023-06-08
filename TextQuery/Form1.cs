using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using TextQueryLib;

namespace TextQuery
{
    public partial class Form1 : Form
    {
        private static void SelectTab(TabControl tabControl, string key)
        {
            var index = tabControl.TabPages.IndexOfKey(key);
            if (index >= 0)
            {
                tabControl.SelectedIndex = index;
            }
        }

        public Form1()
        {
            InitializeComponent();
            SelectTab(this.dataSourceTabs, (string)Properties.Settings.Default["DataSourceTab"]);
            SelectTab(this.processorTabControl, (string)Properties.Settings.Default["ProcessorTab"]);
            this.fileNameTextBox.Text = (string)Properties.Settings.Default["LastFile"];
            this.regexTextBox.Text = (string)Properties.Settings.Default["LastRegex"];
            this.wholeMatchCheckBox.Checked = (bool)(Properties.Settings.Default["RegexWholeMatch"] ?? false);
            this.regexGlobalCheckBox.Checked = (bool)(Properties.Settings.Default["RegexGlobal"] ?? false);
            this.queryTextBox.Text = string.IsNullOrEmpty((string)Properties.Settings.Default["LastQuery"]) ? this.queryTextBox.Text : (string)Properties.Settings.Default["LastQuery"];
            this.Width = (int)(Properties.Settings.Default["WinWidth"] ?? this.Width);
            this.Height = (int)(Properties.Settings.Default["WinHeight"] ?? this.Height);
            this.splitContainer1.SplitterDistance = (int)(Properties.Settings.Default["WinSplitterSize"] ?? this.splitContainer1.SplitterDistance);
            this.csvTsvComboBox.SelectedItem = string.IsNullOrEmpty((string)Properties.Settings.Default["CsvTsv"]) ? "CSV" : Properties.Settings.Default["CsvTsv"];
            this.csvTsvHeadingsTextBox.Text = (string)Properties.Settings.Default["CsvTsvHeadings"];
            this.csvTsvHeadingsCheckBox.Checked = (bool)(Properties.Settings.Default["CsvTsvUseFileHeadings"] ?? false);
            this.multipleFilePathTextBox.Text = (string)Properties.Settings.Default["MultipleFileDirectory"];
            this.multipleFileNamePatternTextBox.Text = (string)Properties.Settings.Default["MultipleFilePattern"];
            this.multipleFileRecursiveCheckBox.Checked = (bool)(Properties.Settings.Default["MultipleFileRecursive"] ?? false);
            this.saveProfileFileDialog.FileName = (string)Properties.Settings.Default["LastProfile"];
            this.saveProfileFileDialog.InitialDirectory = Path.GetDirectoryName((string)Properties.Settings.Default["LastProfile"]);
            this.openProfileFileDialog.FileName = (string)Properties.Settings.Default["LastProfile"];
            this.openProfileFileDialog.InitialDirectory = Path.GetDirectoryName((string)Properties.Settings.Default["LastProfile"]);
        }

        private AllData GetActiveDataSource()
        {
            switch (this.dataSourceTabs.SelectedTab.Name)
            {
                case "textTab":
                    var memoryStream = new MemoryStream();
                    var writer = new StreamWriter(memoryStream);
                    writer.Write(this.textTextBox.Text);
                    writer.Flush();
                    memoryStream.Position = 0;
                    return new AllData(memoryStream);
                case "singleFileTab":
                    return new AllData(File.OpenRead(fileNameTextBox.Text));
                case "multipleFileTab":
                    var directory = new DirectoryInfo(multipleFilePathTextBox.Text);
                    var fileInfos = directory.EnumerateFiles(this.multipleFileNamePatternTextBox.Text, this.multipleFileRecursiveCheckBox.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    return new AllData(fileInfos.Select(f => new NamedStream(f.FullName)).ToArray());
                default:
                    throw new Exception($"Unsupported data source tab: {this.dataSourceTabs.SelectedTab.Name}");
            }
        }

        private class AsyncState : IAsyncState
        {
            private readonly BackgroundWorker worker;
            private readonly DoWorkEventArgs eventArgs;

            public AsyncState(BackgroundWorker worker, DoWorkEventArgs eventArgs)
            {
                this.worker = worker;
                this.eventArgs = eventArgs;
            }

            public bool IsCancellationPending()
            {
                if (worker.CancellationPending)
                {
                    this.eventArgs.Cancel = true;
                    return true;
                }

                return false;
            }

            public void ReportStatus(int percentage, string message) => worker.ReportProgress(percentage, message);
        }

        private ScanResult Scan(QueryParams param, IAsyncState asyncState)
        {
            if (param.RegexParams != null)
            {
                return ScanFile.ProcessWithRegex(param.RegexParams.Regex, param.Data, param.RegexParams.IncludeWholeMatch, param.RegexParams.Global, asyncState);
            }
            else
            {
                throw new Exception($"No supported processor parameters found");
            }
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            if (queryBackgroundWorker.IsBusy)
            {
                queryBackgroundWorker.CancelAsync();
            }
            else
            {
                queryButton.Text = "Cancel";
                toolStripStatusLabel1.Text = "Working...";
                var param = new QueryParams(queryTextBox.Text, GetActiveDataSource());
                switch (this.processorTabControl.SelectedTab.Name)
                {
                    case "regexTab":
                        param.RegexParams = new QueryParams.RegexParam(regexTextBox.Text, wholeMatchCheckBox.Checked, regexGlobalCheckBox.Checked);
                        break;
                    case "csvTsvTab":
                        throw new Exception("TODO");
                    default:
                        throw new Exception($"Unsupported processor tab: {this.processorTabControl.SelectedTab.Name}");
                }
                queryBackgroundWorker.RunWorkerAsync(param);
            }
        }

        // TODO: Use the JSON params instead and move at least some of this code into the lib
        private class QueryParams
        {
            public QueryParams(string query, AllData data)
            {
                this.Query = query;
                this.Data = data;
            }

            public RegexParam? RegexParams { get; set; }

            public string Query { get; }

            public AllData Data { get; }

            public class RegexParam
            {
                public RegexParam(string regexExpr, bool includeWholeMatch, bool global)
                {
                    Regex = regexExpr;
                    IncludeWholeMatch = includeWholeMatch;
                    Global = global;
                }

                public string Regex { get; }

                public bool IncludeWholeMatch { get; }

                public bool Global { get; }
            }
        }

        private void queryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var param = (QueryParams)e.Argument!;
            ScanResult initialResult;
            var asyncState = new AsyncState((BackgroundWorker)sender, e);
            using (param.Data)
            {
                initialResult = Scan(param, asyncState);
            }

            e.Result = ScanFile.QueryScanResult(initialResult, param.Query);
        }

        private void queryBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                toolStripStatusLabel1.Text = (string)e.UserState;
            }
        }

        private void queryBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            queryButton.Text = "Query";
            toolStripProgressBar1.Visible = false;

            if (e.Error != null)
            {
                toolStripStatusLabel1.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                toolStripStatusLabel1.Text = "Cancelled";
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                var result = (ScanResult)e.Result!;
                dataGridView1.ColumnCount = result.Columns.Count;
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Name = result.Columns[i].Title;
                }

                foreach (var row in result.Rows)
                {
                    dataGridView1.Rows.Add(CreateRowData(row, result));
                }

                dataGridView1.AutoResizeColumns();

                toolStripStatusLabel1.Text = "Done";
            }
        }

        private static string[] CreateRowData(ScanRow row, ScanResult result)
        {
            var array = new string[result.Columns.Count];
            for (int i = 0; i < result.Columns.Count; i++)
            {
                array[i] = row.Data[result.Columns[i].Title];
            }
            return array;
        }

        private void escapeButton_Click(object sender, EventArgs e)
        {
            var escaped = Regex.Escape(regexTextBox.SelectedText);
            var start = regexTextBox.SelectionStart;
            regexTextBox.Text = $"{regexTextBox.Text.Substring(0, start)}{escaped}{regexTextBox.Text.Substring(start + regexTextBox.SelectionLength)}";
            regexTextBox.Focus();
            regexTextBox.SelectionStart = start;
            regexTextBox.SelectionLength = escaped.Length;
        }

        private void queryTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default["DataSourceTab"] = this.dataSourceTabs.SelectedTab.Name;
            Properties.Settings.Default["ProcessorTab"] = this.processorTabControl.SelectedTab.Name;
            Properties.Settings.Default["LastFile"] = this.fileNameTextBox.Text;
            Properties.Settings.Default["LastRegex"] = this.regexTextBox.Text;
            Properties.Settings.Default["RegexWholeMatch"] = this.wholeMatchCheckBox.Checked;
            Properties.Settings.Default["RegexGlobal"] = this.regexGlobalCheckBox.Checked;
            Properties.Settings.Default["LastQuery"] = this.queryTextBox.Text;
            Properties.Settings.Default["WinWidth"] = this.Width;
            Properties.Settings.Default["WinHeight"] = this.Height;
            Properties.Settings.Default["WinSplitterSize"] = this.splitContainer1.SplitterDistance;
            Properties.Settings.Default["CsvTsv"] = (string)this.csvTsvComboBox.SelectedItem;
            Properties.Settings.Default["CsvTsvHeadings"] = this.csvTsvHeadingsTextBox.Text;
            Properties.Settings.Default["CsvTsvUseFileHeadings"] = this.csvTsvHeadingsCheckBox.Checked;
            Properties.Settings.Default["MultipleFileDirectory"] = this.multipleFilePathTextBox.Text;
            Properties.Settings.Default["MultipleFilePattern"] = this.multipleFileNamePatternTextBox.Text;
            Properties.Settings.Default["MultipleFileRecursive"] = this.multipleFileRecursiveCheckBox.Checked;
            Properties.Settings.Default["LastProfile"] = this.saveProfileFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        private void csvTsvHeadingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            csvTsvHeadingsTextBox.Enabled = !csvTsvHeadingsCheckBox.Checked;
            editHeadingsButton.Enabled = !csvTsvHeadingsCheckBox.Checked;
        }

        private void openSingleFileButton_Click(object sender, EventArgs e)
        {
            this.openSingleFileDialog.FileName = this.fileNameTextBox.Text;
            this.openSingleFileDialog.InitialDirectory = Path.GetDirectoryName(this.fileNameTextBox.Text);
            switch (this.openSingleFileDialog.ShowDialog(this))
            {
                case DialogResult.OK:
                    this.fileNameTextBox.Text = this.openSingleFileDialog.FileName;
                    return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://learn.microsoft.com/en-us/azure/data-explorer/kusto/query/",
                UseShellExecute = true
            });
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            switch (this.saveProfileFileDialog.ShowDialog())
            {
                case DialogResult.OK:
                    try
                    {
                        this.openProfileFileDialog.FileName = this.saveProfileFileDialog.FileName;
                        this.openProfileFileDialog.InitialDirectory = this.saveProfileFileDialog.InitialDirectory;
                        using (var stream = this.saveProfileFileDialog.OpenFile())
                        {
                            JsonSerializer.Serialize(stream, new ScanProfile
                            {
                                Source =
                                    this.dataSourceTabs.SelectedTab.Name switch
                                    {
                                        "textTab" => null,
                                        "singleFileTab" => new FileDataSourceProfile { File = this.fileNameTextBox.Text },
                                        "multipleFileTab" => null,////new FilesDataSourceProfile { },
                                        _ => throw new Exception($"Cannot serialize data source tab: {this.dataSourceTabs.SelectedTab.Name}")
                                    },
                                Processor =
                                    this.processorTabControl.SelectedTab.Name switch
                                    {
                                        "regexTab" => new RegexProcessorProfile { Regex = this.regexTextBox.Text, IncludeWholeMatch = this.wholeMatchCheckBox.Checked, Global = this.regexGlobalCheckBox.Checked },
                                        //"csvTsvTab" => new ...
                                        _ => throw new Exception($"Cannot serialize processor tab: {this.processorTabControl.SelectedTab.Name}")
                                    },
                                Query = new KustoProfile { Query = this.queryTextBox.Text }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        this.toolStripStatusLabel1.Text = ex.Message;
                    }
                    break;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            switch (this.openProfileFileDialog.ShowDialog())
            {
                case DialogResult.OK:
                    try
                    {
                        this.saveProfileFileDialog.InitialDirectory = this.openProfileFileDialog.InitialDirectory;
                        using (var stream = this.openProfileFileDialog.OpenFile())
                        {
                            var profile = JsonSerializer.Deserialize<ScanProfile>(stream);
                            switch (profile?.Source)
                            {
                                default:
                                case null:
                                    // TODO
                                    ////SelectTab(this.dataSourceTabs, "textTab");
                                    break;
                                case FileDataSourceProfile fileProfile:
                                    SelectTab(this.dataSourceTabs, "singleFileTab");
                                    this.fileNameTextBox.Text = fileProfile.File ?? this.fileNameTextBox.Text;
                                    break;
                                case FilesDataSourceProfile filesProfile:
                                    SelectTab(this.dataSourceTabs, "multipleFileTab");
                                    this.multipleFilePathTextBox.Text = filesProfile.Path ?? this.multipleFilePathTextBox.Text;
                                    this.multipleFileNamePatternTextBox.Text = filesProfile.Pattern ?? this.multipleFileNamePatternTextBox.Text;
                                    break;
                            }

                            switch (profile?.Processor)
                            {
                                case RegexProcessorProfile regexProfile:
                                    SelectTab(this.processorTabControl, "regexTab");
                                    this.regexTextBox.Text = regexProfile.Regex ?? this.regexTextBox.Text;
                                    this.wholeMatchCheckBox.Checked = regexProfile.IncludeWholeMatch ?? this.wholeMatchCheckBox.Checked;
                                    this.regexGlobalCheckBox.Checked = regexProfile.Global ?? this.regexGlobalCheckBox.Checked;
                                    break;
                            }

                            switch (profile?.Query)
                            {
                                case KustoProfile kustoProfile:
                                    this.queryTextBox.Text = kustoProfile.Query ?? this.queryTextBox.Text;
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.toolStripStatusLabel1.Text = ex.Message;
                    }
                    break;
            }
        }

        private void multipleFileBrowseButton_Click(object sender, EventArgs e)
        {
            this.multipleFileFolderBrowserDialog.InitialDirectory = this.multipleFilePathTextBox.Text;
            switch (this.multipleFileFolderBrowserDialog.ShowDialog(this))
            {
                case DialogResult.OK:
                    this.multipleFilePathTextBox.Text = this.multipleFileFolderBrowserDialog.SelectedPath;
                    break;
            }
        }
    }
}