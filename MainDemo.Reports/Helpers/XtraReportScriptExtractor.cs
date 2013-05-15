using System;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportScriptExtractor
    {
        public XtraReportScriptExtractor(XtraReportLoader loader)
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
}
