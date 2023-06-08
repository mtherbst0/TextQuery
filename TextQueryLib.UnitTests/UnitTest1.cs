using Moq;

namespace TextQueryLib.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ScanFile_ProcessWithRegex_UnnamedGroups()
        {
            var asyncState = new Mock<IAsyncState>();
            using (var data = CreateData("abc123\ndef456"))
            {
                var result = ScanFile.ProcessWithRegex("^([a-zA-Z]+)([0-9]+)$", data, false, false, asyncState.Object);

                Assert.AreEqual(2, result.Columns.Count);
                Assert.IsTrue(result.Columns.Any(col => col.Title == "1"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "2"));

                Assert.AreEqual(2, result.Rows.Count);
                Assert.IsTrue(result.Rows.Any(row => row.Data["1"] == "abc" && row.Data["2"] == "123"));
                Assert.IsTrue(result.Rows.Any(row => row.Data["1"] == "def" && row.Data["2"] == "456"));
            }
        }

        [TestMethod]
        public void ScanFile_ProcessWithRegex_NoMatch()
        {
            var asyncState = new Mock<IAsyncState>();
            using (var data = CreateData(""))
            {
                var result = ScanFile.ProcessWithRegex("^([a-zA-Z]+)([0-9]+)$", data, false, false, asyncState.Object);

                Assert.AreEqual(2, result.Columns.Count);
                Assert.IsTrue(result.Columns.Any(col => col.Title == "1"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "2"));

                Assert.AreEqual(0, result.Rows.Count);
            }
        }

        [TestMethod]
        public void ScanFile_ProcessWithRegex_IncludeWholeMatch()
        {
            var asyncState = new Mock<IAsyncState>();
            using (var data = CreateData("abc123\ndef456"))
            {
                var result = ScanFile.ProcessWithRegex("^([a-zA-Z]+)([0-9]+)$", data, true, false, asyncState.Object);

                Assert.AreEqual(3, result.Columns.Count);
                Assert.IsTrue(result.Columns.Any(col => col.Title == "0"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "1"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "2"));

                Assert.AreEqual(2, result.Rows.Count);
                Assert.IsTrue(result.Rows.Any(row => row.Data["0"] == "abc123" && row.Data["1"] == "abc" && row.Data["2"] == "123"));
                Assert.IsTrue(result.Rows.Any(row => row.Data["0"] == "def456" && row.Data["1"] == "def" && row.Data["2"] == "456"));
            }
        }

        [TestMethod]
        public void ScanFile_ProcessWithRegex_NamedGroups()
        {
            var asyncState = new Mock<IAsyncState>();
            using (var data = CreateData("abc123\ndef456"))
            {
                var result = ScanFile.ProcessWithRegex("^(?<alpha>[a-zA-Z]+)(?<num>[0-9]+)$", data, false, false, asyncState.Object);

                Assert.AreEqual(2, result.Columns.Count);
                Assert.IsTrue(result.Columns.Any(col => col.Title == "alpha"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "num"));

                Assert.AreEqual(2, result.Rows.Count);
                Assert.IsTrue(result.Rows.Any(row => row.Data["alpha"] == "abc" && row.Data["num"] == "123"));
                Assert.IsTrue(result.Rows.Any(row => row.Data["alpha"] == "def" && row.Data["num"] == "456"));
            }
        }

        [TestMethod]
        public void ScanFile_QueryScanResult_BasicQuery()
        {
            var asyncState = new Mock<IAsyncState>();
            using (var data = CreateData("abc123\ndef456"))
            {
                var scanResult = ScanFile.ProcessWithRegex("^(?<alpha>[a-zA-Z]+)(?<num>[0-9]+)$", data, false, false, asyncState.Object);
                var result = ScanFile.QueryScanResult(scanResult, "result | where alpha == 'abc' | project alpha, number = num");

                Assert.AreEqual(2, result.Columns.Count);
                Assert.IsTrue(result.Columns.Any(col => col.Title == "alpha"));
                Assert.IsTrue(result.Columns.Any(col => col.Title == "number"));

                Assert.AreEqual(1, result.Rows.Count);
                Assert.IsTrue(result.Rows.Any(row => row.Data["alpha"] == "abc" && row.Data["number"] == "123"));
            }
        }

        private static AllData CreateData(string str)
        {
            var memorydata = new MemoryStream();
            var writer = new StreamWriter(memorydata);
            writer.Write(str);
            writer.Flush();
            memorydata.Position = 0;
            return new AllData(memorydata);
        }
    }
}