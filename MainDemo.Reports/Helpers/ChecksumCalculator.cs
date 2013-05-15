using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MainDemo.Reports
{
    public class ChecksumCalculator
    {
        public const string ChecksumPrefix = "// Checksum:";

        public ChecksumCalculator(string repxFileName)
        {
            if (!File.Exists(repxFileName))
                throw new IOException(String.Format("File {0} does not exist", repxFileName));

            Contents = File.ReadAllText(repxFileName);           
        }

        private string Contents { get; set; }

        public string Get()
        {
            string hash;
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5ComputeHash = md5.ComputeHash(Encoding.UTF8.GetBytes(Contents));
                hash = BitConverter.ToString(md5ComputeHash).Replace("-", String.Empty);
            }
            return hash;
        }
    }
}