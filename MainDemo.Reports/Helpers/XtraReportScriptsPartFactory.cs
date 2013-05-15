using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Text;

namespace MainDemo.Reports
{
    [TestFixture]
    public class XtraReportScriptsPartFactory_Tests
    {
        [Test]
        public void Test_GetFullSourceCode_ContactsGroupedByPosition()
        {
            var factory = new XtraReportScriptsPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            string source = factory.GetScriptsCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var factory = new XtraReportScriptsPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx");
            string source = factory.GetScriptsCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportScriptsPartFactory
    {
        public XtraReportScriptsPartFactory(string repxFileName)
        {
            if (!File.Exists(repxFileName))
                throw new IOException(String.Format("File {0} does not exist", repxFileName));

            Contents = File.ReadAllText(repxFileName);
            ScriptExtractor = new ScriptExtractorFactory().CreateSlowScriptExtractor(repxFileName);
            NameSpace = new UniqueIdentifierProvider(repxFileName).NameSpace;
        }

        private string NameSpace { get; set; }
        private string Contents { get; set; }
        private IScriptExtractor ScriptExtractor { get; set; }        

        private string GetReportName()
        {
            int indexOfName = Contents.IndexOf(@"public class ");
            string result = Contents;
            result = result.Substring(indexOfName + @"public class ".Length);
            result = result.Substring(0, result.IndexOf(":")).Trim();
            return result;
        }

        private string GetScriptSection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(XtraReportSyncMarkers.StartMarker);
            sb.AppendLine(ScriptExtractor.ExtractScripts());
            sb.AppendLine(XtraReportSyncMarkers.EndMarker);
            sb.AppendLine();
            return sb.ToString();
        }

        public string GetScriptsCode()
        {
            string scriptSection = GetScriptSection();
            var parser = new XtraReportScriptParser();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("namespace MainDemo.Reports." + NameSpace + " {");
            sb.AppendLine(parser.CollectUsingReferences(scriptSection));
            sb.AppendLine("    public partial class " + GetReportName());
            sb.AppendLine("    {");
            sb.AppendLine(parser.RemoveUsingReferences(scriptSection));
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
