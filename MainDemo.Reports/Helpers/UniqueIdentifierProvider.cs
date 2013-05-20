using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace MainDemo.Reports
{
    public class UniqueIdentifierProvider
    {
        public UniqueIdentifierProvider(string repxFileName)
        {
            if (repxFileName == null)
                throw new ArgumentNullException("repxFileName");

            RepxFileName = repxFileName;
        }

        private string RepxFileName { get; set; }

        private string _ClassName;
        public string ClassName
        {
            get
            {
                if (_ClassName == null)
                {
                    _ClassName = CreateValidIdentifierFromString(Path.GetFileNameWithoutExtension(RepxFileName));
                    _ClassName = "_" + _ClassName; // prevents conflicts when contained XtraReport has the same name
                }
                return _ClassName;
            }
        }

        private string _NameSpace;
        public string NameSpace
        {
            get
            {
                if (_NameSpace == null)
                {
                    string directoryNameWithoutRoot = Path.GetDirectoryName(RepxFileName.Substring(Path.GetPathRoot(RepxFileName).Length));
                    var parts = directoryNameWithoutRoot.Split(Path.DirectorySeparatorChar);
                    var validParts = parts.Select(x => CreateValidIdentifierFromString(x));
                    _NameSpace = String.Join(".", validParts);
                    _NameSpace = _NameSpace + "." + ClassName;
                }
                return _NameSpace;
            }
        }

        private string CreateValidIdentifierFromString(string name)
        {
            // See http://blog.visualt4.com/2009/02/creating-valid-c-identifiers.html

            Regex regex = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");

            string result = regex.Replace(name.Trim(), "_");

            // The identifier must start with a character 
            if (!char.IsLetter(result, 0))
                result = string.Concat("_", result);

            // If the identifier happens to be a keyword, add an underscore
            if (!Microsoft.CSharp.CSharpCodeProvider.CreateProvider("C#").IsValidIdentifier(result))
                result = string.Concat("_", result);

            return result;
        }
    }
}