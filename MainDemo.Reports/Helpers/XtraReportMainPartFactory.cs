using System;
using System.Linq;
using System.Reflection;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using NUnit.Framework;
using System.Text;
using System.IO;
using DevExpress.XtraReports.Serialization;
using System.Text.RegularExpressions;
using System.Resources;
using System.Globalization;

namespace MainDemo.Reports
{
    [TestFixture]
    public class XtraReportMainPartFactory_Tests
    {
        [Test]
        public void Test_GetScripts_AreNotNull()
        {
            var contentsExtractor = new XtraReportContentsExtractor(@"C:\Projects\Coprocess\Version 13.2\DotNet\NetDA\Reports\ABB\FXExposureReport.repx");
            var scriptExtractor = new XtraReportScriptExtractor(new ResourceStringDeserializer());
            string scripts = scriptExtractor.ExtractScripts(contentsExtractor.Contents);
            Console.WriteLine(scripts);
            Assert.IsNotNull(scripts);
        }

        [Test]
        public void Test_GetFullSourceCode_ContactsGroupedByPosition()
        {
            var contentsExtractor = new XtraReportContentsExtractor(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            var scriptExtractor = new XtraReportScriptExtractor(new ResourceStringDeserializer());
            var factory = new XtraReportMainPartFactory(contentsExtractor);
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var contentsExtractor = new XtraReportContentsExtractor(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx");
            var scriptExtractor = new XtraReportScriptExtractor(new ResourceStringDeserializer());
            var factory = new XtraReportMainPartFactory(contentsExtractor);
            string source = factory.GetMainCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportScriptExtractor_Slow
    {        
        public XtraReportScriptExtractor_Slow(XtraReportLoader loader)
        {
            if (loader == null)
                throw new ArgumentNullException("loader");

            Report = loader.Report;
        }

        public XtraReport Report { get; private set; }

        public string ExtractScripts()
        {
            return Report.ScriptsSource;
        }
    }

    public class XtraReportScriptExtractor
    {
        public XtraReportScriptExtractor(ResourceStringDeserializer deserializer)
        {
            if (deserializer == null)
                throw new ArgumentNullException("deserializer");
            Deserializer = deserializer;
        }

        public ResourceStringDeserializer Deserializer { get; private set; }

        private string GetResourceStringFromContent(string contents)
        {
            string resourceLine = contents;
            int startIndex = resourceLine.IndexOf("string resourceString = ");
            StringBuilder resources = new StringBuilder();
            while (startIndex > -1)
            {
                resourceLine = resourceLine.Substring(startIndex + "resourceString = ".Length);
                string resourceLinePart = resourceLine.Substring(0, resourceLine.IndexOf(";"));
                if (resourceLinePart != null)
                {
                    resources.Append(String.Join("", resourceLinePart.Split('"').Where(x => !(new Regex(@"\s").IsMatch(x)))));
                }
                startIndex = resourceLine.IndexOf("resourceString += ");
            }
            string result = resources.ToString();
            char a = result[0];
            char z = result[result.Length - 1];
            return result.ToString();
        }

        public string ExtractScripts(string content)
        {
            string resourceString = GetResourceStringFromContent(content);
            if (resourceString != null)
                return Deserializer.Deserialize(resourceString);
            return null;
        }
    }

    public class ResourceStringDeserializer
    {
        public string Deserialize(string resourceString)
        {
            XRResourceManager resourceManager = new XRResourceManager(resourceString);
            return resourceManager.GetString("$this.ScriptsSource");
        }
    }

    public class XtraReportContentsExtractor
    {
        public XtraReportContentsExtractor(string repxFileName)
        {
            if (repxFileName == null)
                throw new ArgumentNullException("repxFileName");

            if (!File.Exists(repxFileName))
                throw new IOException(String.Format("File {0} was not found.", repxFileName));

            Contents = File.ReadAllText(repxFileName);
        }

        public string Contents { get; private set; }
    }

    public class XtraReportMainPartFactory
    {
        public XtraReportMainPartFactory(XtraReportContentsExtractor contentsExtractor)
        {
            if (contentsExtractor == null)
                throw new ArgumentNullException("contentsExtractor");

            Contents = contentsExtractor.Contents;
        }

        public XtraReport Report { get; private set; }
        public string Contents { get; private set; }

        public string GetMainCode()
        {
            string result = Contents;
            result = result.Replace(@"public class ", @"partial class ");
            result = result.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            result = result.Replace(@" : DevExpress.XtraReports.UI.XtraReport {", @" : DevExpress.ExpressApp.Reports.XafReport {");
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