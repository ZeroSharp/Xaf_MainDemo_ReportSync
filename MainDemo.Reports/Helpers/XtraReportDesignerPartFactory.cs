using System;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportDesignerPartFactory
    {
        public XtraReportDesignerPartFactory(XtraReportReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            Contents = reader.Contents;
            NameSpace = new UniqueIdentifierProvider(reader.RelativeRepxFileName).NameSpace;
        }

        public string Contents { get; private set; }
        public string NameSpace { get; private set; }

        public string GetMainCode()
        {
            string result = Contents;
            result = result.Replace("namespace XtraReportSerialization {", "namespace MainDemo.Reports." + NameSpace + " {");
            result = result.Replace(@"public class ", @"partial class ");
            result = result.Replace(@" : DevExpress.XtraReports.UI.XtraReport {", @" : DevExpress.ExpressApp.Reports.XafReport {");
            result = result.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            return result;
        }
    }

    public class XtraReportResourceFactory
    {
        public string GetResxContents()
        {
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("MainDemo.Reports.Resources.ResxTemplate.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}