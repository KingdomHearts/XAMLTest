using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

using Xamarin.Auth;

namespace XAMLTest.Droid
{
    [Activity(Label = "YesHugo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

                // Previewer only code  
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
                LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            Intent startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
            startMain.SetFlags(ActivityFlags.NewTask);
            StartActivity(startMain);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            
            // Retrieve the user's email address
            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);

            // Display it on the UI
            //var googleButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            //googleButton.Text = $"Connected with {email}";
            
        }

        public void OnAuthenticationCanceled()
        {
            
            new AlertDialog.Builder(this)
                           .SetTitle("Authentication canceled")
                           .SetMessage("You didn't completed the authentication process")
                           .Show();
                           
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
           
            new AlertDialog.Builder(this)
                           .SetTitle(message)
                           .SetMessage(exception?.ToString())
                           .Show();
                           
        }

        public static GoogleAuthenticator Auth;
        public void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            StartActivity(intent);
        }

    }
}