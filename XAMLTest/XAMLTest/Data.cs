using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms.Internals;

namespace XAMLTest
{
    class Data
    {
        
        public Data()
        {

        }
        public void GetProfileData()
        {
            bool fileExists = File.Exists("XML/Profiles.xml");
            string fileName = "Profiles.xml"; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.xml");
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            bool fileExists1 = File.Exists(filePath);
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("XAMLTest.Profiles.xml");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            if (fileExists)
            {
                XDocument doc = XDocument.Load("pack://application:,,,/Profile.xml");
                var authors = doc.Descendants("Author");
                foreach (var author in authors)
                {
                    Console.WriteLine(author.Value);
                }
            }

        }

        public void GetTimeLineData()
        {

        }
    }
}
