using System;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportScriptInjector
    {
        public XtraReportScriptInjector(string repxFileName)
        {
            if (repxFileName == null)
                throw new ArgumentNullException("repxFileName");

            var loader = new XtraReportLoader(new XtraReport(), repxFileName);
            Report = loader.Report;
        }

        public string RepxFileName { get; private set; }
        public XtraReport Report { get; private set; }

        public void InjectScripts(string fullSourceCode, string repxFileName, bool replaceOriginal = false)
        {
            if (fullSourceCode == null)
                throw new ArgumentNullException("fullSourceCode");
            if (!fullSourceCode.Contains(XtraReportSyncMarkers.StartMarker))
                throw new ArgumentException("fullSourceCode does not contain a valid ScriptSourceStartMarker");
            if (!fullSourceCode.Contains(XtraReportSyncMarkers.EndMarker))
                throw new ArgumentException("fullSourceCode does not contain a valid ScriptSourceEndMarker");

            XtraReportScriptParser parser = new XtraReportScriptParser();
            string collectedUsingReferences = parser.CollectUsingReferences(fullSourceCode);
            string scriptSource = parser.RemoveIgnoredSections(fullSourceCode);
            scriptSource = Environment.NewLine + collectedUsingReferences + Environment.NewLine + scriptSource;

            var temporaryReportFileName = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), ".repx");
            Report.ScriptsSource = scriptSource;
            try
            {
                Report.SaveLayout(temporaryReportFileName);
                AssertReportContainsScriptSource(temporaryReportFileName, scriptSource);
                if (replaceOriginal)
                    File.Copy(temporaryReportFileName, repxFileName, true);
            }
            finally
            {
                File.Delete(temporaryReportFileName);
            }
        }

        private void AssertReportContainsScriptSource(string repxFileName, String expectedScriptSource)
        {
            var reader = new XtraReportScriptExtractor(new XtraReportLoader(new XtraReport(), repxFileName));
            string source = reader.ExtractScripts();
            var parser = new XtraReportScriptParser();
            if (parser.RemoveIgnoredSections(expectedScriptSource) != parser.RemoveIgnoredSections(source))
                throw new Exception("Expected scripts source does not match.");
        }
    }
}