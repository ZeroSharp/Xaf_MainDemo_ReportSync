using System;
using System.IO;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportLoader
    {
        public XtraReportLoader(XtraReport report, string repxFileName)
        {
            if (repxFileName == null)
                throw new ArgumentNullException("repxFileName");
            if (report == null)
                throw new ArgumentNullException("report");

            if (!File.Exists(repxFileName))
                throw new IOException(String.Format("File {0} was not found.", repxFileName));

            Report = report;
            Report.LoadLayout(repxFileName);
        }

        public XtraReport Report { get; private set; }
    }
}
