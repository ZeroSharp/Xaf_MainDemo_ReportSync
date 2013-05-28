// Checksum:2DF056067AE38CA4AAF091D901041588



namespace MainDemo.Reports.MainDemo_Module.EmbeddedReports._TasksStateReport {
    public partial class _TasksStateReport
    {
        // -- Anything above this line will be ignored when converting back to repx. --
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
        // -- Anything below this line will be ignored when converting back to repx. --
    }
}

