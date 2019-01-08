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
using XAMLTest.Views.ApiPages;

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

        IList<TimeLineData> TimeLineDatas;
    public TimeLine ()
        {
            TimeLineDatas = new ObservableCollection<TimeLineData>();

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
                TimeLineDatas.Add(timeLineData[i]);
            }
            BindingContext = TimeLineDatas;

        }

        async void OnItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
        private void timelineListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            timelineListView.SelectedItem = null;
            Navigation.PushAsync(new Calendar());
        }

        private void PressMeButton_Pressed(object sender, EventArgs e)
        {
            App.NavigationMasterDetail(new Profile());
        }

    }
}