using System;
using System.IO;
using System.Linq;

namespace MainDemo.Reports
{
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
}
