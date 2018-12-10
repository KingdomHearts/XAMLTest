using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMLTest.Views.MainMenu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            XAMLTest.Data.MockData mockData = new Data.MockData();
            ModelsProfile modelsProfile = mockData.GetProfileData(usernameEntry.Text);
            if (modelsProfile.Password == passwordEntry.Text)
            {
                //Hier een show naar TimeLine Page
                TimeLine TL = new TimeLine();
                //[System.CodeDom.Compiler.GeneratedCodeAttribute("")]
                //MainPage = new NavigationPage (new TimeLine());
                //await NavigationPage.  //.PushAsync(new TimeLine());
                await Navigation.PushAsync(new TimeLine());
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
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };
        }

    }
}
