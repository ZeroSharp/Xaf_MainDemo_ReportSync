using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace MainDemo.Reports
{
    [TestFixture]
    public class XtraReportMainPartFactory_Tests
    {
        [Test]
        public void Test_GetScripts_AreNotNull()
        {
            var scriptExtractor = new ScriptExtractorFactory().CreateResourceScriptExtractor(@"C:\Projects\Coprocess\Version 13.2\DotNet\NetDA\Reports\ABB\FXExposureReport.repx");
            string scripts = scriptExtractor.ExtractScripts();
            Console.WriteLine(scripts);
            Assert.IsNotNull(scripts);
        }

        [Test]
        public void Test_GetScriptsWithResourceScriptExtractor_AreNotNull()
        {
            var scriptExtractor = new ScriptExtractorFactory().CreateSlowScriptExtractor(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            string scripts = scriptExtractor.ExtractScripts();
            Console.WriteLine(scripts);
            Assert.IsNotNull(scripts);
        }

        [Test]
        public void Test_GetFullSourceCode_ContactsGroupedByPosition()
        {
            var factory = new XtraReportDesignerPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var factory = new XtraReportDesignerPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx");
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportDesignerPartFactory
    {
        public XtraReportDesignerPartFactory(string repxFileName)
        {
            if (repxFileName == null)
                throw new ArgumentNullException("repxFileName");

            Contents = new XtraReportContentsExtractor(repxFileName).Contents;
            NameSpace = new UniqueIdentifierProvider(repxFileName).NameSpace;
        }

        public string Contents { get; private set; }
        public string NameSpace { get; private set; }

        public string GetMainCode()
        {
            string result = Contents;
            result = result.Replace("namespace XtraReportSerialization {", "namespace MainDemo.Reports." + NameSpace + " {");
            result = result.Replace(@"public class ", @"partial class ");
            result = result.Replace(@" : DevExpress.XtraReports.UI.XtraReport {", @" : DevExpress.ExpressApp.Reports.XafReport {");
            result = result.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            return result;
        }
    }

    public class XtraReportResourceFactory
    {
        public string GetResxContents()
        {
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("MainDemo.Reports.Resources.ResxTemplate.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}