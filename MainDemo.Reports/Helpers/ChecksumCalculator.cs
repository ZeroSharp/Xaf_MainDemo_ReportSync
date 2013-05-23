using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MainDemo.Reports
{
    public static class ChecksumCalculator
    {
        public static string Get(string content)
        {
            var parser = new ChecksumParser();
            string parsedContent = parser.RemoveChecksums(content);

            string hash;
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5ComputeHash = md5.ComputeHash(Encoding.UTF8.GetBytes(parsedContent));
                hash = BitConverter.ToString(md5ComputeHash).Replace("-", String.Empty);
            }
            return hash;
        }
    }
}