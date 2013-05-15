using System;
using System.Linq;
using DevExpress.XtraReports.Serialization;

namespace MainDemo.Reports
{
    public class ResourceStringDeserializer
    {
        public string Deserialize(string resourceString)
        {
            XRResourceManager resourceManager = new XRResourceManager(resourceString);
            return resourceManager.GetString("$this.ScriptsSource");
        }
    }
}
