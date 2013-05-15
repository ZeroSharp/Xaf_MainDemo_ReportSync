using System;
using System.Linq;

namespace MainDemo.Reports
{
    public static class AssertReportScriptsEx
    {
        private static XtraReportDesignerPartFactory CreateReportHelperReader(string reportFileName)
        {
            return new XtraReportDesignerPartFactory(reportFileName);
        }

        private static string RemoveIgnoredSections(string fullSourceCode)
        {
            var parser = new XtraReportScriptParser();
            return parser.RemoveIgnoredSections(fullSourceCode);
        }

        public static void AssertReportContainsScriptSource(string reportFileName, String expectedScriptSource)
        {
            XtraReportDesignerPartFactory reportHelper = CreateReportHelperReader(reportFileName);
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
