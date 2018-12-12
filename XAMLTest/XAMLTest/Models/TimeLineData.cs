using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XAMLTest.Models
{
    public class TimeLineData
    {
        public Image ProfileImage { get; set; }
        public string ProfileName { get; set; }
        public string TimeLineTime { get; set; }
        public string TimeLineTekst { get; set; }
        public string TimeLineLikes { get; set; }

        public string TimeLineHead { get { return this.ProfileName + " " + this.TimeLineTime; } }

        public TimeLineData()
        {
        }
        public TimeLineData(string pProfileName, string pTimeLineTekst, string pTimeLineTime)
        {
            //pProfileImage = ProfileImage;
            ProfileName = pProfileName;
            TimeLineTime = pTimeLineTime;
            // pTimeLineTime = TimeLineTime;
            TimeLineTekst = pTimeLineTekst;
            // pTimeLineLikes = TimeLineLikes;
        }

    }
}
