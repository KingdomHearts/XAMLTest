using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMLTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
            };

            //Hier een show naar TimeLine Page
            TimeLine TL = new TimeLine();
            //[System.CodeDom.Compiler.GeneratedCodeAttribute("")]
            //MainPage = new NavigationPage (new TimeLine());
            //await NavigationPage.  //.PushAsync(new TimeLine());
            await Navigation.PushAsync(new TimeLine());

        }
        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };
        }

    }
}
