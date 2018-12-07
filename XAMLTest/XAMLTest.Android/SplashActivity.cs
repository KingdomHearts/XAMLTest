﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace XAMLTest.Droid
{
    [Activity(Label = "XAMLTest", Icon ="@drawable/hugo", Theme= "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startup = new Task(StartUp);
            startup.Start();
            
        }

        async void StartUp()
        {
            await Task.Delay(5);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

    }
}