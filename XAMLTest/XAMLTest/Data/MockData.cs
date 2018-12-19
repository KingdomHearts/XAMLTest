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
using Xamarin.Forms;
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
            new MainMenuItem { Navigationid = 1, Icon = "Menu2.png", ClassName = "Views.MenuPages.MainEvents", Text = "Evenementen" },
            new MainMenuItem { Navigationid = 2, Icon = "Menu3.png", ClassName = "Views.MenuPages.MainGroups", Text = "Groepen" },
            new MainMenuItem { Navigationid = 3, Icon = "Menu4.png", ClassName = "Views.MenuPages.MenuAgenda", Text = "Besparing" },
            new MainMenuItem { Navigationid = 4, Icon = "Menu5.png", ClassName = "Views.ApiPages.Calendar", Text = "Agenda" },
            new MainMenuItem { Navigationid = 5, Icon = "Menu6.png", ClassName = "Views.MenuPages.MenuSavings", Text = "Wijzig gegevens" }
            };
        }

        public List<TimeLineData> GetTimeLineData()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimeLine" + User.UserName + ".xml");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockDataTimeLine());
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            XmlNodeList aNodes = xmlDocument.GetElementsByTagName("TimeLineProfile");
            List<TimeLineData> timeLines = new List<TimeLineData>();
            Image image = new Image();
            int index = 0;
            foreach (XmlNode xmlNode in aNodes)
            {

                XmlNode node = xmlDocument.GetElementsByTagName("TimeLineProfile").Item(index);

                TimeLineData timeLine = new TimeLineData();
                foreach (XmlNode item in node.ChildNodes)
                {
                    if ((item).NodeType == XmlNodeType.Element)
                    {
                        //Get the Element value here
                        if ((((item).FirstChild)) != null)
                        {

                            string value = ((item).FirstChild).Value;
                            if (item.Name == "ProfileImage")
                            {
                                image.Source = value;
                                timeLine.ProfileImage = image;
                            }
                            if (item.Name == "ProfileName")
                            {
                                timeLine.ProfileName = value;
                            }
                            if (item.Name == "TimeLineTime")
                            {
                                timeLine.TimeLineTime = value;
                            }
                            if (item.Name == "TimeLineTekst")
                            {
                                timeLine.TimeLineTekst = value;
                            }
                            if (item.Name == "TimeLineLikes")
                            {
                                timeLine.TimeLineLikes = value;
                            }
                        }
                    }
                }
                index++;
                timeLines.Add(timeLine);
            }
            return timeLines;
        }

        public void AddToTimeLineData()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimeLine" + User.UserName + ".xml");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockDataTimeLine());
            }
        }

        public void EditXmlProfileData(string pAttribute, string pValue)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockDataProfile());
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            XmlNodeList aNodes = xmlDocument.GetElementsByTagName("Profile");

            int indexNodes = 0;
            int index = 0;
            bool isBreak = false;
            // loop through all AID nodes
            foreach (XmlNode aNode in aNodes)
            {

                // check if that attribute even exists...
                XmlNode node = xmlDocument.GetElementsByTagName("Profile").Item(index);
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
                            if (item.Name == "UserName")
                            {
                                if (value == User.UserName)
                                {
                                    for (int nodeCount = 0; nodeCount < node.ChildNodes.Count; nodeCount++)
                                    {
                                        if (node.ChildNodes[nodeCount].Name == pAttribute)
                                        {
                                            
                                            if (node.ChildNodes[nodeCount].Name == "Friends")
                                            {
                                                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "Name", null);
                                                newNode.InnerText = pValue;
                                                node.ChildNodes[nodeCount].AppendChild(newNode);
                                                break;
                                            }
                                            node.ChildNodes[nodeCount].InnerText = pValue;
                                            isBreak = true;
                                            break;
                                        }
                                        if ((nodeCount+1) == node.ChildNodes.Count)
                                        {
                                            XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, pAttribute, null);
                                            newNode.InnerText = pValue;
                                            aNode.AppendChild(newNode);
                                            //newNode.AppendChild(node);
                                            //xmlDocument.DocumentElement.AppendChild(newNode);
                                            isBreak = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (isBreak)
                    {
                        break;
                    }
                    indexNodes++;
                }
                if (isBreak)
                {
                    break;
                }
                index++;
            }
            xmlDocument.Save(fileName);

            Stream stream = File.Open(fileName, FileMode.Open);
            using (var reader = new System.IO.StreamReader(stream))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader.ReadToEnd());
                string test = "";
            }

            }
        public string CreateXmlMockDataProfile()
        {
            XElement contacts =
            new XElement("Profiles",
                new XElement("Profile",
                new XElement("Name", "Go"),
                    new XElement("FirstName", "Hu"),
                    new XElement("UserName", "Hugo"),
                    new XElement("Bio", "Ik ben Hugo en ik zal je helpen op de weg!"),
                    new XElement("Email", "Test@email"),
                    new XElement("Password", "HugoApplication"),
                    new XElement("Friends",
                        new XElement("Name", "Jessica Berent"),
                        new XElement("Name", "Elias Jansen"),
                        new XElement("Name", "Michael Bossink")),
                        new XElement("Groups",
                            new XElement("Name", "Shell"),
                            new XElement("Name", "Starbucks"),
                            new XElement("Name", "Curious Cockatoo"))),
                new XElement("Profile",
                new XElement("Name", "Bossink"),
                    new XElement("FirstName", "Michael"),
                    new XElement("UserName", "michaelbos"),
                    new XElement("Bio", "Ik ben Michael en ben een programmeur"),
                    new XElement("Email", "Test1@email"),
                    new XElement("Password", "testing"),
                    new XElement("Friends",
                        new XElement("Name", "Jessica Berent"),
                        new XElement("Name", "Elias Jansen"),
                        new XElement("Name", "Hugo")),
                        new XElement("Groups",
                            new XElement("Name", "Shell"),
                            new XElement("Name", "Starbucks"),
                            new XElement("Name", "Curious Cockatoo")))
            );
            return contacts.ToString();
        }
        public string CreateXmlMockDataTimeLine()
        {
            XElement contacts =
            new XElement("TimeLine",
                new XElement("TimeLineProfile",
                new XElement("ProfileImage", ""),
                    new XElement("ProfileName", "Hugo"),
                    new XElement("TimeLineTime", "5 minuten geleden"),
                    new XElement("TimeLineTekst", "Je hebt geweldig gereden! Maar liefst een 9.8"),
                    new XElement("TimeLineLikes", "231")),
                new XElement("TimeLineProfile",
                new XElement("ProfileImage", ""),
                    new XElement("ProfileName", "Elias"),
                    new XElement("TimeLineTime", "12 minuten geleden"),
                    new XElement("TimeLineTekst", "Elias nodigd je uit om mee te doen aan een evenement"),
                    new XElement("TimeLineLikes", "10")),
                new XElement("TimeLineProfile",
                new XElement("ProfileImage", ""),
                    new XElement("ProfileName", "Kirsten"),
                    new XElement("TimeLineTime", "30 minuten geleden"),
                    new XElement("TimeLineTekst", "Lekker aan het carpoolen met Olivier"),
                    new XElement("TimeLineLikes", "420")),
                new XElement("TimeLineProfile",
                new XElement("ProfileImage", ""),
                    new XElement("ProfileName", "Demy"),
                    new XElement("TimeLineTime", "1 dag geleden"),
                    new XElement("TimeLineTekst", "Demy heeft gedeeld dat hij jou heeft ingehaald op de ranglijst voor A1 rijder"),
                    new XElement("TimeLineLikes", "1"))
            );
            return contacts.ToString();
        }

        public void CreateProfileData(string pUserName, string pPassword)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.xml");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, CreateXmlMockDataProfile());
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

                File.WriteAllText(fileName, CreateXmlMockDataProfile());
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
                File.WriteAllText(fileName, CreateXmlMockDataProfile());
            }
            string returning = CreateXmlMockDataProfile();
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
                                if (item.Name == "Friends")
                                {
                                    //int friendIndex = 0;
                                    XmlNode friendsNodes = doc.GetElementsByTagName("Friends").Item(index);
                                    List<string> friends = new List<string>();
                                    foreach (XmlNode friend in friendsNodes.ChildNodes)
                                    {
                                        if ((friend).NodeType == XmlNodeType.Element)
                                        {
                                            //Get the Element value here
                                            if ((((friend).FirstChild)) != null)
                                            {
                                                string friendName = ((friend).FirstChild).Value;
                                                if (friend.Name == "Name")
                                                {
                                                    friends.Add(friendName);
                                                }
                                            }
                                        }
                                        //friendIndex++;
                                    }
                                    profile.Friends = friends;
                                    friends = new List<string>();
                                }
                                if (item.Name == "Groups")
                                {
                                    //int GroupIndex = 0;
                                    XmlNode GroupNodes = doc.GetElementsByTagName("Groups").Item(index);
                                    List<string> groups = new List<string>();
                                    foreach (XmlNode Group in GroupNodes.ChildNodes)
                                    {
                                        if ((Group).NodeType == XmlNodeType.Element)
                                        {
                                            //Get the Element value here
                                            if ((((Group).FirstChild)) != null)
                                            {
                                                string GroupName = ((Group).FirstChild).Value;
                                                if (Group.Name == "Name")
                                                {
                                                    groups.Add(GroupName);
                                                }
                                            }
                                        }
                                        //GroupIndex++;
                                    }
                                    profile.Groups = groups;
                                    groups = new List<string>();
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
