using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using XAMLTest.ViewModels;
using XAMLTest.Views.MenuPages;
using XAMLTest.Models;
using XAMLTest.Views.ApiPages;


using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using ListView = Xamarin.Forms.ListView;

namespace XAMLTest.Views.MainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuMaster : ContentPage
    {
        public ListView ListView;
        private MainMenuMasterViewModel _model;
        public MainMenuMaster()
        {
            InitializeComponent();

            _model = new MainMenuMasterViewModel();
            BindingContext = _model;
            ListView = MenuItemsListView;


        }


        private async void MenuItemListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MainMenuItem;
            if (item == null)
            {

                Console.WriteLine("item is null");
                return;
            }
            
            switch (item.Text)
            {
                case "Evenementen":
                     App.NavigationMasterDetail(new MainEvents());
                    break;
                case "Groepen":
                     App.NavigationMasterDetail(new MainGroups());
                    break;
                case "Besparing":
                     App.NavigationMasterDetail(new MenuSavings());
                    break;
                case "Agenda":
                     App.NavigationMasterDetail(new Calendar());
                    break;
                case "Wijzig gegevens":
                     App.NavigationMasterDetail(new MenuOptions());
                    break;
                default:
                    App.NavigationMasterDetail(new TimeLine());
                    break;
            }

        }
    }
}