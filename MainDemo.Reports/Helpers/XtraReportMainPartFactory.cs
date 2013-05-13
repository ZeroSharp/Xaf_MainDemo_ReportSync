using System;
using System.Linq;
using System.Reflection;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using NUnit.Framework;
using System.Text;
using System.IO;

namespace MainDemo.Reports
{
    [TestFixture]
    public class XtraReportMainPartFactory_Tests
    {
        [Test]
        public void Test_GetFullSourceCode_ContactsGroupedByPosition()
        {
            var loader = new XtraReportLoader(new XtraReport(), @"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            var factory = new XtraReportMainPartFactory(loader);
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var loader = new XtraReportLoader(new XtraReport(), @"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx");
            var factory = new XtraReportMainPartFactory(loader);
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportMainPartFactory
    {
        public XtraReportMainPartFactory(XtraReportLoader loader)
        {
            if (loader == null)
                throw new ArgumentNullException("loader");

            Report = loader.Report;
            Contents = loader.Contents;
        }

        public XtraReport Report { get; set; }
        public string Contents { get; set; }

        public string GetMainCode()
        {
            string result = Contents.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            result = EmbedScripts(result);
            return result;
        }

        private string GetScriptSection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(XtraReportSyncMarkers.StartMarker);
            sb.AppendLine(Report.ScriptsSource);
            sb.AppendLine(XtraReportSyncMarkers.EndMarker);
            sb.AppendLine();
            return sb.ToString();
        }

        private string EmbedScripts(string result)
        {
            int index = result.LastIndexOf('}', result.Length - 1);
            index = result.LastIndexOf('}', index - 1);
            if (index >= 0)
                result = result.Insert(index, GetScriptSection());
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