using System;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportScriptsPartFactory
    {
        public XtraReportScriptsPartFactory(XtraReportReader reader, UniqueIdentifierProvider uniqueIdentifierProvider)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (uniqueIdentifierProvider == null)
                throw new ArgumentNullException("uniqueIdentifierProvider");

            Contents = reader.Contents;
            ScriptExtractor = new XtraReportScriptExtractor(new XtraReportLoader(new XtraReport(), reader.FullRepxFileName));
            
            NameSpace = uniqueIdentifierProvider.NameSpace;
            ClassName = uniqueIdentifierProvider.ClassName;
        }

        private string NameSpace { get; set; }
        private string ClassName { get; set; }
        private string Contents { get; set; }
        private XtraReportScriptExtractor ScriptExtractor { get; set; }        

        private string GetScriptSection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(XtraReportSyncMarkers.StartMarker);
            var parser = new XtraReportScriptParser();
            sb.AppendLine(parser.MaximumUnindent(ScriptExtractor.ExtractScripts()));
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
            sb.AppendLine(parser.CollectUsingReferences(scriptSection));
            sb.AppendLine();
            sb.AppendLine("namespace " + NamespaceHelper.Get() + "." + NameSpace + " {");
            sb.AppendLine("    public partial class " + ClassName);
            sb.AppendLine("    {");
            sb.AppendLine(parser.RemoveUsingReferences(scriptSection));
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
