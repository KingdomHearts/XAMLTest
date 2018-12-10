using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using XAMLTest.Data;
using XAMLTest.Models;
using XAMLTest.Views.MainMenu;


using Xamarin.Forms.Internals;
using Xamarin.Forms;

namespace XAMLTest.ViewModels
{
    public class MainMenuMasterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MainMenuItem> ObserverableCollection { get; set; }

        public MockData Data { get; set; }

        public ObservableCollection<MainMenuItem> MenuItems { get; set; }

        public MainMenuMasterViewModel()
        {
            Data = new MockData();
            MenuItems = Data.MainMenuItems;
        }

        public void MenuItemTapped(MainMenuItem item)
        {
            var mdp = App.Current.MainPage as MasterDetailPage;
            var nmspace = App.Instance.GetType().Namespace;
            var clsname = string.Format("{0}.{1}", nmspace, item.ClassName);
            Type pageType = Type.GetType(clsname, true);
            var page = (Page)Activator.CreateInstance(pageType);
            page.Title = item.Text;
            if (mdp != null)
            {
                mdp.Detail = new NavigationPage(page);
                mdp.IsPresented = false;
            }
            else
            {
                Console.WriteLine("mdp");
            }
           
        }
    }
}
