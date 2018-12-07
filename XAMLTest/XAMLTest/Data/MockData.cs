using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using XAMLTest.Models;

namespace XAMLTest.Data
{
    public class MockData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MainMenuItem> MainMenuItems { get; set; }

        public MockData()
        {
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            MainMenuItems = new ObservableCollection<MainMenuItem>()
            {
            new MainMenuItem { NavigationID = 1, Icon = "", ClassName = "Views.MainMenu.MainMenuDetail", Text = "Evenementen" },
            new MainMenuItem { NavigationID = 2, Icon = "", ClassName = "Views.MainMenu.MainMenuDetail", Text = "Groepen" },
            new MainMenuItem { NavigationID = 3, Icon = "", ClassName = "Views.MainMenu.MainMenuDetail", Text = "Besparing" },
            new MainMenuItem { NavigationID = 4, Icon = "", ClassName = "Views.MainMenu.MainMenuDetail", Text = "Agenda" },
            new MainMenuItem { NavigationID = 5, Icon = "", ClassName = "Views.MainMenu.MainMenuDetail", Text = "Wijzig gegevens" }
            };
        }

        public void GetProfileData()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
        }
    }
}
