using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace MainDemo.Reports
{
    public class XtraReportValidator
    {
        public XtraReportValidator(XtraReport xtraReport)
        {
            if (xtraReport == null)
                throw new ArgumentNullException("xtraReport");

            Report = xtraReport;
        }

        public XtraReport Report { get; private set; }

        public CompilerErrorCollection CompilerMessages { get; private set; }

        private IEnumerable<CompilerError> GetCompilerErrors()
        {
            CompilerMessages = Report.ValidateScripts();
            return CompilerMessages
                    .Cast<CompilerError>()
                    .Where(e => !e.IsWarning);
        }

        private IEnumerable<CompilerError> _Errors;
        public IEnumerable<CompilerError> Errors
        {
            get
            {
                if (_Errors == null)
                    _Errors = GetCompilerErrors();
                return _Errors;
            }
        }

        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }
        }
    }
}
