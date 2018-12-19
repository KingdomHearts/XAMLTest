using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMLTest.Views.MainMenu;
using XAMLTest.Data;
using XAMLTest.Models;

public class MyNiceObject
{
    public string AdvancedInfo { get; set; }
    public string FullName { get; set; }
    public string Number { get; set; }
    public MyNiceObject(string name, string number)
    {
        FullName = name;
        Number = number;
    }
}

namespace XAMLTest.Views.MainMenu
{

public partial class TimeLine : ContentPage
{
    public TimeLine ()
        {

            InitializeComponent();
            MockData mockData = new MockData();
            List<TimeLineData> timeLineData = mockData.GetTimeLineData();
            //aantal records in de database
            int dynamicTestValue = timeLineData.Count; //This is the "dynamic" value 
            TimeLineData mObject;
            List<TimeLineData> mObjectList = new List<TimeLineData>();
            for (int i = 0; i < dynamicTestValue; i++)
            {
                //Ingevuld worden door een database
                mObject = new TimeLineData(timeLineData[i].ProfileName,timeLineData[i].TimeLineTekst, timeLineData[i].TimeLineTime);
                //mObject.AdvancedInfo = "MyAdvancedInfo" + i;
                mObjectList.Add(mObject);

            }

            //TimeLineView.ItemsSource = mObjectList;


            //var template = new DataTemplate(typeof(Label));
            //template.SetBinding(Label.TextProperty, "ProfileName");
            //template.SetBinding(Label.TextProperty, "TimeLineTime");
            //template.SetBinding(Label.TextProperty, "TimeLineTekst");
            
            var template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, "TimeLineHead");
            //template.SetBinding(TextCell.DetailProperty, "TimeLineTime");
            template.SetBinding(TextCell.DetailProperty, "TimeLineTekst");
            //template.SetBinding(TextCell.DetailProperty, "AdvancedInfo");
            
            //TimeLineView.ItemTemplate = template;
        }

        async void OnItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
    }
}