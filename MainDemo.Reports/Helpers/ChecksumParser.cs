using System;
using System.Linq;

namespace MainDemo.Reports
{
    public class ChecksumParser
    {
        public const string ChecksumPrefix = "// Checksum:";

        public string RemoveChecksums(string contents)
        {
            if (contents == null)
                return null;

            return String.Join(Environment.NewLine,
                            contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                            .Where(line => !IsChecksum(line))
                          );
        }

        private bool IsChecksum(string line)
        {
            return line.TrimStart().StartsWith(ChecksumPrefix);
        }
    }
}
