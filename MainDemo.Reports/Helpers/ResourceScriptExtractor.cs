using System;
using System.Linq;
using System.Text;

namespace MainDemo.Reports
{
    public class ResourceScriptExtractor : IScriptExtractor
    {
        public ResourceScriptExtractor(ResourceStringDeserializer deserializer, string content)
        {
            if (deserializer == null)
                throw new ArgumentNullException("deserializer");
            if (content == null)
                throw new ArgumentNullException("content");

            Deserializer = deserializer;
            Content = content;
        }

        public ResourceStringDeserializer Deserializer { get; private set; }
        public string Content { get; set; }

        private string GetResourceStringFromContent(string contents)
        {
            string resourceLine = contents;
            int startIndex = resourceLine.IndexOf("string resourceString = ");
            StringBuilder resources = new StringBuilder();
            while (startIndex > -1)
            {
                resourceLine = resourceLine.Substring(startIndex + "resourceString = ".Length);
                string resourceLinePart = resourceLine.Substring(0, resourceLine.IndexOf(";"));
                if (resourceLinePart != null)
                {
                    //resources.Append(String.Join("", resourceLinePart.Split('"').Where(x => !(new Regex(@"\s").IsMatch(x)))));
                    resources.Append(String.Join("", resourceLinePart.Split('"').Where(x => !x.Contains(" + "))));
                }
                startIndex = resourceLine.IndexOf("resourceString += ");
            }
            string result = resources.ToString();
            //Assert.That(result.StartsWith("z"));
            //Assert.That(result.EndsWith("A"));
            return result;
        }

        public string ExtractScripts()
        {
            string resourceString = GetResourceStringFromContent(Content);
            if (resourceString != null)
                return Deserializer.Deserialize(resourceString);
            return null;
        }
    }
}
