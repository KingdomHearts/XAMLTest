using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMLTest.Views.MainMenu;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XAMLTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

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
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
