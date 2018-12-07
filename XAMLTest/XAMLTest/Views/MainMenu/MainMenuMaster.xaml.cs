using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using XAMLTest.ViewModels;
using XAMLTest.Models;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private void MenuItemListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MainMenuItem;
            if (item == null)
            {
                return;
            }

            _model.MenuItemTapped(item);
        }
    }
}