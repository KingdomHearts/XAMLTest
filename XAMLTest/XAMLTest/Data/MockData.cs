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
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockData());
            }

            XmlDocument xmlProfileDoc = new XmlDocument();
            xmlProfileDoc.Load(fileName);
            XmlElement ParentElement = xmlProfileDoc.CreateElement("Profile");
            XmlElement Name = xmlProfileDoc.CreateElement("Name");
            Name.InnerText = "";
            XmlElement FirstName = xmlProfileDoc.CreateElement("FirstName");
            FirstName.InnerText = "";
            XmlElement UserName = xmlProfileDoc.CreateElement("UserName");
            UserName.InnerText = pUserName;
            XmlElement Bio = xmlProfileDoc.CreateElement("Bio");
            Bio.InnerText = "";
            XmlElement Email = xmlProfileDoc.CreateElement("Email");
            Email.InnerText = "";
            XmlElement Password = xmlProfileDoc.CreateElement("Password");
            Password.InnerText = pPassword;

            ParentElement.AppendChild(Name);
            ParentElement.AppendChild(FirstName);
            ParentElement.AppendChild(UserName);
            ParentElement.AppendChild(Email);
            ParentElement.AppendChild(Bio);
            ParentElement.AppendChild(Password);
            xmlProfileDoc.DocumentElement.AppendChild(ParentElement);
            xmlProfileDoc.Save(fileName);

            FileStream file = File.Open(fileName, FileMode.Open);
            using (var reader = new StreamReader(file))
            {
                string read = reader.ReadToEnd();
                string test = "";
            }
        }
        
        public List<ModelsProfile> GetProfilesData()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            if (!File.Exists(fileName))
            {

                File.WriteAllText(fileName, CreateXmlMockData());
            }
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

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockData());
            }
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
