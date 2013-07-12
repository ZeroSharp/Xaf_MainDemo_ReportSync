using System;
using System.Configuration;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using System.Threading;
using DevExpress.ExpressApp.AuditTrail;
using MainDemo.Module;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Core;
using System.Globalization;
using DevExpress.ExpressApp.MiddleTier;
using DevExpress.ExpressApp.Security.ClientServer;

namespace MainDemo.Win.Mdi {
	public class Program {
        static private void winApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e) {
            e.FormattingCulture = CultureInfo.GetCultureInfo("en-US");
		}
        [STAThread]
		public static void Main(string[] arguments) {
			MainDemoWinApplication winApplication = new MainDemoWinApplication();
#if DEBUG
                DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            winApplication.CustomizeFormattingCulture += new EventHandler<CustomizeFormattingCultureEventArgs>(winApplication_CustomizeFormattingCulture);
            try {
				AuditTrailService.Instance.QueryCurrentUserName += new QueryCurrentUserNameEventHandler(Instance_QueryCurrentUserName);
				winApplication.LastLogonParametersReading += new EventHandler<LastLogonParametersReadingEventArgs>(winApplication_LastLogonParametersReading);
                winApplication.CreateCustomModelDifferenceStore += new EventHandler<CreateCustomModelDifferenceStoreEventArgs>(winApplication_CreateCustomModelDifferenceStore);
				if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
					winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				}
                winApplication.CreateCustomObjectSpaceProvider += delegate(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
                    e.ObjectSpaceProvider = new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)winApplication.Security, e.ConnectionString, e.Connection);
                };
                winApplication.Setup();
				winApplication.Start();
			} catch(Exception e) {
				winApplication.HandleException(e);
			}
		}
        static void winApplication_CreateCustomModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
			string modelDiffsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            e.Store = new FileModelStore(modelDiffsPath, "Mdi.Model");
            e.Handled = true;
        }
		static void winApplication_LastLogonParametersReading(object sender, LastLogonParametersReadingEventArgs e) {
			if(string.IsNullOrEmpty(e.SettingsStorage.LoadOption("", "UserName"))) {
				e.SettingsStorage.SaveOption("", "UserName", "Sam");
			}
		}
		static void Instance_QueryCurrentUserName(object sender, QueryCurrentUserNameEventArgs e) {
			e.CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		}
	}
}
