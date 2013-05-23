using System;
using System.IO;
using System.Linq;

namespace MainDemo.Reports
{
    public static class ChecksumExtractor
    {
        public static string Get(string file)
        {
            if (!File.Exists(file))
                return null;

            string firstLine = ReadFirstLine(file);

            if (!firstLine.Contains(ChecksumParser.ChecksumPrefix))
                return null;

            return firstLine.Substring(firstLine.IndexOf(ChecksumParser.ChecksumPrefix) + ChecksumParser.ChecksumPrefix.Length);
        }

        private static string ReadFirstLine(string file)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                return reader.ReadLine();
            }
        }
    }
}
