using System;
using System.IO;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;

namespace MainDemo.Reports
{
    //public class UniqueIdentifierProvider
    //{
    //    private string CreateValidIdentifierFromString(string name)
    //    {
    //        // See http://blog.visualt4.com/2009/02/creating-valid-c-identifiers.html

    //        Regex regex = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");

    //        string result = regex.Replace(name, "_");

    //        // The identifier must start with a character 
    //        if (!char.IsLetter(result, 0))
    //            result = string.Concat("_", result);

    //        // If the identifier happens to be a keyword, add an underscore
    //        if (!Microsoft.CSharp.CSharpCodeProvider.CreateProvider("C#").IsValidIdentifier(result))
    //            result = string.Concat("_", result);

    //        return result;
    //    }

    //    public string CreateUniqueIdentifier(string repxFileName)
    //    {
    //        return CreateValidIdentifierFromString(repxFileName);
    //    }
    //}

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