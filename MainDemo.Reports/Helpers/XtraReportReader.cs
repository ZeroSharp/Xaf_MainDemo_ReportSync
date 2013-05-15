using System;
using System.IO;
using System.Linq;

namespace MainDemo.Reports
{
    public class XtraReportReader
    {
        public XtraReportReader(string solutionDirectory, string relativeRepxFileName)
        {
            if (solutionDirectory == null)
                throw new ArgumentNullException("solutionDirectory");
            if (!Directory.Exists(solutionDirectory))
                throw new IOException(String.Format("Directory {0} does not exist", solutionDirectory));

            SolutionDirectory = solutionDirectory;

            if (relativeRepxFileName == null)
                throw new ArgumentNullException("relativeRepxFileName");
            RelativeRepxFileName = relativeRepxFileName;

            FullRepxFileName = solutionDirectory + relativeRepxFileName;

            if (!File.Exists(FullRepxFileName))
                throw new IOException(String.Format("File {0} does not exist", FullRepxFileName));
        }

        public string SolutionDirectory { get; private set; }
        public string RelativeRepxFileName { get; private set; }
        public string FullRepxFileName { get; private set; }

        private string _Contents;
        public string Contents
        {
            get
            {
                if (_Contents == null)
                    _Contents = File.ReadAllText(FullRepxFileName);
                return _Contents;
            }
        }
    }
}
