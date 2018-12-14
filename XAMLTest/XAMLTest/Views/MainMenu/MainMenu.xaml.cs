using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAMLTest.Views.MainMenu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainMenu : MasterDetailPage
	{
		public MainMenu ()
		{
            InitializeComponent();
		    this.Master = new MainMenuMaster();
		    this.Detail = new NavigationPage(new MainPage());
            App.MasterDetails = this;
		}
        
	}

}