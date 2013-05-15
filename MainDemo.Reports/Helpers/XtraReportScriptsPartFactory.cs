using System;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportScriptsPartFactory
    {
        public XtraReportScriptsPartFactory(XtraReportReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            Contents = reader.Contents;
            ScriptExtractor = new XtraReportScriptExtractor(new XtraReportLoader(new XtraReport(), reader.FullRepxFileName));
            NameSpace = new UniqueIdentifierProvider(reader.RelativeRepxFileName).NameSpace;
        }

        private string NameSpace { get; set; }
        private string Contents { get; set; }
        private XtraReportScriptExtractor ScriptExtractor { get; set; }        

        private string GetReportName()
        {
            int indexOfName = Contents.IndexOf(@"public class ");
            string result = Contents;
            result = result.Substring(indexOfName + @"public class ".Length);
            result = result.Substring(0, result.IndexOf(":")).Trim();
            return result;
        }

        private string GetScriptSection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(XtraReportSyncMarkers.StartMarker);
            sb.AppendLine(ScriptExtractor.ExtractScripts());
            sb.AppendLine(XtraReportSyncMarkers.EndMarker);
            sb.AppendLine();
            return sb.ToString();
        }

        public string GetScriptsCode()
        {
            string scriptSection = GetScriptSection();
            var parser = new XtraReportScriptParser();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("namespace MainDemo.Reports." + NameSpace + " {");
            sb.AppendLine(parser.CollectUsingReferences(scriptSection));
            sb.AppendLine("    public partial class " + GetReportName());
            sb.AppendLine("    {");
            sb.AppendLine(parser.RemoveUsingReferences(scriptSection));
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
