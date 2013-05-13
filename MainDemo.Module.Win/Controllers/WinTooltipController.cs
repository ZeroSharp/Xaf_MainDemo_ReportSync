using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;

namespace MainDemo.Module.Win.Controllers {
   public partial class WinTooltipController : ViewController {
      public WinTooltipController() {
         InitializeComponent();
         RegisterActions(components);
      }

      private void WinTooltipController_ViewControlsCreated(object sender, EventArgs e) {
         GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
         if (listEditor != null) {
            foreach (GridColumn column in listEditor.GridView.Columns) {
               column.ToolTip = "Click to sort by " + column.Caption;
            }
         }
      }
   }
}
