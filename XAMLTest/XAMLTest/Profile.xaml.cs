using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAMLTest.Data;
using Xamarin.Forms;

namespace XAMLTest
{
	public partial class Profile : ContentPage
	{
		public Profile ()
		{
			InitializeComponent ();
            MockData data = new MockData();
            data.GetProfileData();
            //Get info of the profile
            //ProfileAchievements.
		}
	}
}