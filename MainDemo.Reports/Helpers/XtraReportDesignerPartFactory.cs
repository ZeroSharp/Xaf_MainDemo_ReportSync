using System;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportDesignerPartFactory
    {
        public XtraReportDesignerPartFactory(XtraReportReader reader, UniqueIdentifierProvider uniqueIdentifierProvider)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (uniqueIdentifierProvider == null)
                throw new ArgumentNullException("uniqueIdentifierProvider");

            Contents = reader.Contents;
            
            NameSpace = uniqueIdentifierProvider.NameSpace;
            ClassName = uniqueIdentifierProvider.ClassName;
        }

        public string Contents { get; private set; }
        public string NameSpace { get; private set; }
        public string ClassName { get; private set; }

        private string GetReportName()
        {
            int indexOfName = Contents.IndexOf(@"public class ");
            string result = Contents;
            result = result.Substring(indexOfName + @"public class ".Length);
            result = result.Substring(0, result.IndexOf(":")).Trim();
            return result;
        }

        public string GetMainCode()
        {
            string result = Contents;
            string reportName = GetReportName();
            result = result.Replace("namespace XtraReportSerialization {", "namespace " + NamespaceHelper.Get() + "." + NameSpace + " {" + Environment.NewLine + "partial class " + ClassName + " : DevExpress.ExpressApp.Reports.XafReport {");
            result = result.Replace(@"public class ", @"private DevExpress.XtraReports.UI.XtraReport ");
            result = result.Replace(@"public " + reportName + "()", @"public " + ClassName + "()");
            result = result.Replace(@" : DevExpress.XtraReports.UI.XtraReport {", @";");
            result = result.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            result = result.Replace(@"this.ScriptReferencesString = resources.GetString(""$this.ScriptReferencesString"");", @"//this.ScriptReferencesString = resources.GetString(""$this.ScriptReferencesString"");");
            return result;
        }
    }
}