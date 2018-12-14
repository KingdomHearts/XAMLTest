using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Content.Res;
using JetBrains.Annotations;
using XAMLTest.Data;
using XAMLTest.Models;
using XAMLTest.Views.MainMenu;


using Xamarin.Forms.Internals;
using Xamarin.Forms;

namespace XAMLTest.ViewModels
{
    public class MainMenuMasterViewModel { 
        
        public MockData Data { get; set; }

        public ObservableCollection<MainMenuItem> MenuItems { get; set; }

        public MainMenuMasterViewModel()
        {
            Data = new MockData();
            MenuItems = Data.MainMenuItems;
        }

    }
}
