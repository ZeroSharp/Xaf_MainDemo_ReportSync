using System;
using System.Linq;
using System.IO;
using DevExpress.ExpressApp.Reports;

namespace MainDemo.Reports
{
    public static class AssertReportScriptsEx
    {
        private static XtraReportMainPartFactory CreateReportHelperReader(string reportFileName)
        {
            var contentsExtractor = new XtraReportContentsExtractor(reportFileName);
            var scriptExtractor = new XtraReportScriptExtractor(new ResourceStringDeserializer());
            return new XtraReportMainPartFactory(contentsExtractor);
        }

        private static string RemoveIgnoredSections(string fullSourceCode)
        {
            var parser = new XtraReportScriptParser();
            return parser.RemoveIgnoredSections(fullSourceCode);
        }

        public static void AssertReportContainsScriptSource(string reportFileName, String expectedScriptSource)
        {
            XtraReportMainPartFactory reportHelper = CreateReportHelperReader(reportFileName);
            string source = reportHelper.GetMainCode();
            if (RemoveIgnoredSections(expectedScriptSource) != RemoveIgnoredSections(source))
                throw new Exception("Expected scripts source does not match.");
        }

        //public static void AssertScriptIsInjected(string repxFileName, string scriptsFileName, bool replaceOriginal = false)
        //{
        //    var temporaryReportFileName = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), ".repx");
        //    string modifiedSourceCode = File.ReadAllText(scriptsFileName);

        //    XtraReportMainPartFactory reportHelperWriter = CreateReportHelperReader(repxFileName);
        //    reportHelperWriter.SetFullSourceCode(modifiedSourceCode);
        //    try
        //    {
        //        reportHelperWriter.Report.SaveLayout(temporaryReportFileName);
        //        AssertReportContainsScriptSource(temporaryReportFileName, modifiedSourceCode);
        //        if (replaceOriginal)
        //            File.Copy(temporaryReportFileName, repxFileName, true);
        //    }
        //    finally
        //    {
        //        File.Delete(temporaryReportFileName);
        //    }
        //}
    }
}
