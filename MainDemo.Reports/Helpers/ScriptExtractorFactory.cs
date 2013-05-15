using System;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public interface IScriptExtractor
    {
        string ExtractScripts();
    }

    public class ScriptExtractorFactory
    {
        public IScriptExtractor CreateResourceScriptExtractor(string contents)
        {
            return new ResourceScriptExtractor(new ResourceStringDeserializer(), contents);
        }

        public IScriptExtractor CreateSlowScriptExtractor(string repxFileName)
        {
            return new XtraReportScriptExtractor_Slow(new XtraReportLoader(new XtraReport(), repxFileName));
        }
    }
}
