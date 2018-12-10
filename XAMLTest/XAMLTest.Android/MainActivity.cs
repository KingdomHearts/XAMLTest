using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace XAMLTest.Droid
{
    [Activity(Label = "YesHugo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
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

    }
}