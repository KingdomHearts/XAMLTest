using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XAMLTest.Data
{
    class CalendarMockData
    {

        public static void CreateJsonFile (object pObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(pObject);
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Calendar" + User.UserName + ".json");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllText(fileName, json);
        }

        public static ScheduleAppointmentCollection GetJsonFileCalendar()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Calendar" + User.UserName + ".json");
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ScheduleAppointmentCollection>(json);
                return obj;
            }
            return null;
        }
    }
}
