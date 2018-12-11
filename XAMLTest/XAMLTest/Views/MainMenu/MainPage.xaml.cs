using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using XAMLTest.Data;

namespace XAMLTest.Views.MainMenu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BackgroundImage = "pack://application:,,,/Login6.png";
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            XAMLTest.Data.MockData mockData = new Data.MockData();
            ModelsProfile modelsProfile = mockData.GetProfileData(usernameEntry.Text);
            if (modelsProfile != null)
            {
                if (modelsProfile.Password == passwordEntry.Text)
                {
                    User.UserName = usernameEntry.Text;
                    User.Password = passwordEntry.Text;
                    //Hier een show naar TimeLine Page
                    TimeLine TL = new TimeLine();
                    //[System.CodeDom.Compiler.GeneratedCodeAttribute("")]
                    //MainPage = new NavigationPage (new TimeLine());
                    //await NavigationPage.  //.PushAsync(new TimeLine());
                    await Navigation.PushAsync(new TimeLine());
                }
           
            }
            else
            {
                //Text that login failed

            }
        

        }
        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            XAMLTest.Data.MockData mockData = new Data.MockData();

            mockData.CreateProfileData(usernameEntry.Text, passwordEntry.Text);
        }

    }
}
