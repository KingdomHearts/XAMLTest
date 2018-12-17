﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace XAMLTest.Views.MainMenu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BottomNav : TabbedPage
	{

		public BottomNav ()
		{
		    On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

		    var profNavigationPage = new NavigationPage(new Profile());
            var tlNavigationPage = new NavigationPage(new TimeLine());
		    var rideNavigationPage = new NavigationPage(new RidePage());

		    rideNavigationPage.Title = "Rit";
		    rideNavigationPage.Icon = "Tijdlijn4.png";

            tlNavigationPage.Icon = "Tijdlijn7.png";
		    tlNavigationPage.Title = "Tijdlijn";

		    profNavigationPage.Icon = "Tijdlijn6.png";
		    profNavigationPage.Title = "Profiel";

		    Children.Add(tlNavigationPage);
            Children.Add(rideNavigationPage);
		    Children.Add(profNavigationPage);
        }
	}
}