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

            string firstLine = File.ReadLines(file).First();
            if (!firstLine.Contains(ChecksumCalculator.ChecksumPrefix))
                return null;

            return firstLine.Substring(firstLine.IndexOf(ChecksumCalculator.ChecksumPrefix) + ChecksumCalculator.ChecksumPrefix.Length);
        }
    }
}
