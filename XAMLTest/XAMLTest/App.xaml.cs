using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMLTest.Views.MainMenu;
using System.Threading.Tasks;
using XAMLTest.Views.MenuPages;
using XAMLTest.Data;
using Syncfusion.SfSchedule.XForms;
using Plugin.LocalNotifications;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XAMLTest
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetails { get; set; }

        public static void NavigationMasterDetail(Page page)
        {
            App.MasterDetails.IsPresented = false;
            App.MasterDetails.Detail = new NavigationPage(page);

        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainMenu();
        }
       
        private static volatile App _instance;
        private static object _syncroot = new object();
        public static App Instance
        {
            get {
                if (_instance == null)
                {
                    lock (_syncroot)
                    {
                        if (_instance == null)
                            _instance = new App();
                    }
                }
                return _instance;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            ScheduleAppointmentCollection json = CalendarMockData.GetJsonFileCalendar();
            if (json != null)
            {
                for (int i = 0; i < json.Count; i++)
                {
                    if (json[i].StartTime < DateTime.Now)
                    {
                        Application.Current.Properties["NotificationText"] = "Als je nu vertrekt ben je voor " + json[i].StartTime.TimeOfDay.ToString() + " op je bestemming.";
                        CrossLocalNotifications.Current.Show("Hugo Navigation", Current.Properties["NotificationText"].ToString());
                    }
                }
            }
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static string Notification()
        {
            if (Current.Properties.Count != 0)
            {
                return Application.Current.Properties["NotificationText"].ToString();
            }
            return "";
        }
    }
}
