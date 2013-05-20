using System;
using System.Linq;

namespace MainDemo.Reports
{
    public static class XtraReportSyncMarkers
    {
        public const string StartMarker = "// -- Start of embedded scripts -- ";
        public const string EndMarker = "// -- End of embedded scripts -- ";
    }

    public class XtraReportScriptParser
    {
        private bool IsUsing(string line)
        {
            return line.Trim().StartsWith("using") && line.Contains(";") && !line.Contains("(");
        }
 
        public string CollectUsingReferences(string scriptsSource)
        {
            if (scriptsSource == null)
                return null;

            return String.Join(Environment.NewLine,
                            scriptsSource.Split(new string[]{ Environment.NewLine }, StringSplitOptions.None)
                              .Where(line => IsUsing(line))
                        );
        }

        public string RemoveUsingReferences(string scriptsSource)
        {
            if (scriptsSource == null)
                return null;

            return String.Join(Environment.NewLine,
                            scriptsSource.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                            .Where(line => !IsUsing(line))
                            .Select(line => String.Concat(new String(' ', 8), line))
                          );
        }

        public string RemoveIgnoredSections(string fullSourceCode)
        {
            return String.Join(Environment.NewLine,
                            fullSourceCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                                .SkipWhile(line => !line.Trim().StartsWith(XtraReportSyncMarkers.StartMarker))
                                .Skip(1)
                                .TakeWhile(line => !line.Trim().StartsWith(XtraReportSyncMarkers.EndMarker))
                            );
        }
    }
}
