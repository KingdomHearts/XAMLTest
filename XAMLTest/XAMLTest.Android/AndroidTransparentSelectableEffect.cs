using System;
using System.Collections.Generic;
using System.Text;
using Android.Util;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XAMLTest.Droid;
using XAMLTest.Effects;
using View = Android.Views.View;

[assembly: ResolutionGroupName("XAMLTest")]
[assembly: ExportEffect(typeof(AndroidTransparentSelectableEffect), nameof(TransparentSelectableEffect))]

namespace XAMLTest.Droid
{
    public class AndroidTransparentSelectableEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                TypedValue value = new TypedValue();
                Android.App.Application.Context.Theme.ResolveAttribute(
                    Android.Resource.Attribute.SelectableItemBackground, value, true);
                Control.SetBackgroundResource(value.ResourceId);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
