using System;
using System.Linq;

namespace MainDemo.Reports
{
    //public static class AssertReportScriptsEx
    //{
    //    private statiXtraReportWriterer CreateReportHelperReader(string reportFileName)
    //    {
    //        string uniqueIdentifier = new UniqueIdentifierProvider().CreateUniqueIdentifier(reportFileName);
    //        var loader = new XtraReportLoader(new NettingXafReport(), reportFileName);
    //        var reportHelperReader = neXtraReportWriterer(new XtraReportValidator(loader.Report), uniqueIdentifier);
    //        return reportHelperReader;
    //    }

    //    private static string RemoveIgnoredSections(string fullSourceCode)
    //    {
    //        var parser = new XtraReportScriptParser();
    //        return parser.RemoveIgnoredSections(fullSourceCode);
    //    }

    //    public static void AssertReportContainsScriptSource(string reportFileName, String expectedScriptSource)
    //    {
    //      XtraReportWriterer reportHelper = CreateReportHelperReader(reportFileName);
    //        string source = reportHelper.GetFullSourceCode();
    //        Assert.AreEqual(RemoveIgnoredSections(expectedScriptSource), RemoveIgnoredSections(source), "Expected scripts source does not match.");
    //    }

    //    public static void AssertScriptIsInjected(string repxFileName, string scriptsFileName, bool replaceOriginal = false)
    //    {
    //        var temporaryReportFileName = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), ".repx");
    //        string modifiedSourceCode = File.ReadAllText(scriptsFileName);

    //      XtraReportWriterer reportHelperWriter = CreateReportHelperReader(repxFileName);
    //        reportHelperWriter.SetFullSourceCode(modifiedSourceCode);
    //        try
    //        {
    //            reportHelperWriter.Report.SaveLayout(temporaryReportFileName);
    //            AssertReportContainsScriptSource(temporaryReportFileName, modifiedSourceCode);
    //            if (replaceOriginal)
    //                File.Copy(temporaryReportFileName, repxFileName, true);
    //        }
    //        finally
    //        {
    //            File.Delete(temporaryReportFileName);
    //        }
    //    }
    //}
}
