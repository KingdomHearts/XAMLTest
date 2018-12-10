using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
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

        public string CreateXmlMockData()
        {
            XElement contacts =
            new XElement("Profiles",
                new XElement("Profile",
                new XElement("Name", "Go"),
                    new XElement("FirstName", "Hu"),
                    new XElement("UserName", "Hugo"),
                    new XElement("Bio", "Ik ben Hugo en ik zal je helpen op de weg!"),
                    new XElement("Email", "Test@email"),
                    new XElement("Password", "HugoApplication")),
                new XElement("Profile",
                new XElement("Name", "Bossink"),
                    new XElement("FirstName", "Michael"),
                    new XElement("UserName", "michaelbos"),
                    new XElement("Bio", "Ik ben Michael en ben een programmeur"),
                    new XElement("Email", "Test1@email"),
                    new XElement("Password", "testing"))
            );
            return contacts.ToString();
        }

        public void CreateProfileData(string pUserName, string pPassword)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");

            string xml = CreateXmlMockData();
            File.WriteAllText(fileName, CreateXmlMockData());

            //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Profiles.xml");

            //File.Create(Path);
            // var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            // Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");

            //  bool usable = stream.CanWrite;
            string exitst = "";
           // using (StringWriter str = new StringWriter())
             /*
            using (XmlTextWriter xml = new XmlTextWriter(stream, UTF32Encoding.UTF32))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("Profiles");
                xml.WriteWhitespace("\n");

                // Loop over Tuples.
                foreach (var element in GetPorfilesData())
                {
                    // Write Employee data.
                    xml.WriteStartElement("Profile");

                    xml.WriteElementString("Name", element.Name);
                    xml.WriteElementString("FirstName", element.FirstName);
                    xml.WriteElementString("UserName", element.UserName);
                    xml.WriteElementString("Bio", element.Bio);
                    xml.WriteElementString("Email", element.Email);
                    xml.WriteElementString("Password", element.Password);

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                }

                // End.
                xml.WriteEndElement();
                xml.WriteEndDocument();

                // Result is a string.
                string result = str.ToString();
            }
            */
        }
        
        public List<ModelsProfile> GetProfilesData()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            File.WriteAllText(fileName, CreateXmlMockData());
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");
            Stream stream = File.Open(fileName, FileMode.Open);
            List<ModelsProfile> profiles = new List<ModelsProfile>();
            int index = 0;
            using (var reader = new System.IO.StreamReader(stream))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader.ReadToEnd());

                XmlNodeList xmlNodeList = doc.GetElementsByTagName("Profile");
                foreach (var nodes in xmlNodeList)
                {
                    XmlNode node = doc.GetElementsByTagName("Profile").Item(index);
                    ModelsProfile profile = new ModelsProfile();
                    //Loop through the child nodes
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if ((item).NodeType == XmlNodeType.Element)
                        {
                            //Get the Element value here
                            if ((((item).FirstChild)) != null)
                            {

                                string value = ((item).FirstChild).Value;
                                //Console.WriteLine("Element Value = " + value);
                                if (item.Name == "Name")
                                {
                                    profile.Name = value;
                                }
                                if (item.Name == "FirstName")
                                {
                                    profile.FirstName = value;
                                }
                                if (item.Name == "UserName")
                                {
                                    profile.UserName = value;
                                }
                                if (item.Name == "Bio")
                                {
                                    profile.Bio = value;
                                }
                                if (item.Name == "Email")
                                {
                                    profile.Email = value;
                                }
                                if (item.Name == "Password")
                                {
                                    profile.Password = value;
                                }
                            }
                        }
                    }
                    index++;
                    profiles.Add(profile);
                }
            }
                    return profiles;
        }
        public ModelsProfile GetProfileData(string pUserName)
        {
            ModelsProfile returnProfile;
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            File.WriteAllText(fileName, CreateXmlMockData());
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");
            string returning = CreateXmlMockData();
            Stream stream = File.Open(fileName, FileMode.Open);
            List<ModelsProfile> profiles = new List<ModelsProfile>();
            int index = 0;
            using (var reader = new System.IO.StreamReader(stream))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader.ReadToEnd());

                XmlNodeList xmlNodeList = doc.GetElementsByTagName("Profile");
                foreach (var nodes in xmlNodeList)
                {
                    XmlNode node = doc.GetElementsByTagName("Profile").Item(index);
                    ModelsProfile profile = new ModelsProfile();
                    //Loop through the child nodes
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if ((item).NodeType == XmlNodeType.Element)
                        {
                            //Get the Element value here
                            if ((((item).FirstChild)) != null)
                            {

                                string value = ((item).FirstChild).Value;
                                //Console.WriteLine("Element Value = " + value);
                                if (item.Name == "Name")
                                {
                                    profile.Name = value;
                                }
                                if (item.Name == "FirstName")
                                {
                                    profile.FirstName = value;
                                }
                                if (item.Name == "UserName")
                                {
                                    profile.UserName = value;
                                }
                                if (item.Name == "Bio")
                                {
                                    profile.Bio = value;
                                }
                                if (item.Name == "Email")
                                {
                                    profile.Email = value;
                                }
                                if (item.Name == "Password")
                                {
                                    profile.Password = value;
                                }
                            }
                        }
                    }
                    index++;
                    profiles.Add(profile);
                    if (profile.UserName == pUserName)
                    {
                        returnProfile = profile;
                        return returnProfile;
                    }
                }
                return null;
            }
        }
        
    }
}
