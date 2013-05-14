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
            var factory = new XtraReportScriptsPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx", new XtraReportScriptExtractor(new ResourceStringDeserializer()));
            string source = factory.GetScriptsCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var factory = new XtraReportScriptsPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx", new XtraReportScriptExtractor(new ResourceStringDeserializer()));
            string source = factory.GetScriptsCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportScriptsPartFactory
    {
        public XtraReportScriptsPartFactory(string repxFullName, XtraReportScriptExtractor scriptExtractor)
        {
            if (scriptExtractor == null)
                throw new ArgumentNullException("scriptExtractor");

            ScriptExtractor = scriptExtractor;

            if (!File.Exists(repxFullName))
                throw new IOException(String.Format("File {0} does not exist", repxFullName));

            OriginalContents = File.ReadAllText(repxFullName);
        }

        private string OriginalContents { get; set; }
        private XtraReportScriptExtractor ScriptExtractor { get; set; }

        private string GetScriptSection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(XtraReportSyncMarkers.StartMarker);
            if (OriginalContents.Contains(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");"))
                sb.AppendLine(ScriptExtractor.ExtractScripts(OriginalContents));
            sb.AppendLine(XtraReportSyncMarkers.EndMarker);
            sb.AppendLine();
            return sb.ToString();
        }

        public string GetScriptsCode()
        {
            string scriptSection = GetScriptSection();
            var parser = new XtraReportScriptParser();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace ScriptingNamespace {");
            sb.AppendLine(parser.CollectUsingReferences(scriptSection));
            sb.AppendLine("    public partial class FXExposureReport : DevExpress.XtraReports.ScriptingReportBase {");
            sb.AppendLine(parser.RemoveUsingReferences(scriptSection));
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
