// Checksum:4B9CBE699799E6F8370BE8843A2A0C84



namespace MainDemo.Reports.MainDemo_Module.EmbeddedReports._TasksStateReport {
    public partial class _TasksStateReport
    {
        
        // -- Start of embedded scripts -- 
        
        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            // This is a test
            xrLabel1.Text = "Hello";
        }
        
        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            xrLabel2.Text = GetLabel2Text();
        }
        
        public string GetLabel2Text()
        {
        	return "Label 2!";
        }
        
        // -- End of embedded scripts -- 
        
        
    }
}

