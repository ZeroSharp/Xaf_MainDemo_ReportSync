using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace MainDemo.Reports
{
    [TestFixture]
    public class XtraReportDesignerPartFactory_Tests
    {
        [Test]
        public void Test_GetFullSourceCode_ContactsGroupedByPosition()
        {
            var factory = new XtraReportDesignerPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\ContactsGroupedByPosition.repx");
            string source = factory.GetDesignerCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }

        [Test]
        public void Test_GetFullSourceCode_TasksStateReport()
        {
            var factory = new XtraReportDesignerPartFactory(@"C:\Projects\github\Xaf_MainDemo_ReportSync\MainDemo.Module\EmbeddedReports\TasksStateReport.repx");
            string source = factory.GetDesignerCode();
            Console.WriteLine(source);
            Assert.IsNotNull(source);
        }
    }

    public class XtraReportDesignerPartFactory
    {
        public XtraReportDesignerPartFactory(string repxFullName)
        {
            if (!File.Exists(repxFullName))
                throw new IOException(String.Format("File {0} does not exist", repxFullName));
            Contents = File.ReadAllText(repxFullName);
        }

        private string Contents { get; set; }

        public string GetDesignerCode()
        {
            Contents = Contents.Replace(@"this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");", @"//this.ScriptsSource = resources.GetString(""$this.ScriptsSource"");");
            return Contents;
        }
    }
}
