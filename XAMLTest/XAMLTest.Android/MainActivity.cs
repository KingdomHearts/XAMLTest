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
using Android.Support.V4.App;
using Android.Support.V7.View;
using Xamarin.Auth;
using XAMLTest.Views.ApiPages;
using Plugin.CurrentActivity;

namespace XAMLTest.Droid
{

 
   
    [Activity(Label = "YesHugo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

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



        

        public void CreateNotificationChannel()
        {
            /*
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = "InsertNameHere";
            var channelDescription = "InsertDescriptionHere";
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);

    */



            //CreateNotificationChannel();


            //Intent intent = new Intent(this, typeof(MainActivity));

            //const int pendingIntentID = 0;
            //PendingIntent pendingIntent =
            //PendingIntent.GetActivity(this, pendingIntentID, intent, PendingIntentFlags.OneShot);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                // .SetContentIntent(pendingIntent)
                .SetContentTitle("Hugo Navigatie")
                .SetContentText(App.Notification())
                .SetSmallIcon(Android.Resource.Drawable.IcNotificationOverlay);

            Notification notification = builder.Build();
            notification.Defaults |= NotificationDefaults.Vibrate | NotificationDefaults.Sound;

            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;


            notificationManager.Notify(NOTIFICATION_ID, notification);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}