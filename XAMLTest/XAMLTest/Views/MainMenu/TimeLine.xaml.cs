using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMLTest.Views.MainMenu;

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
            //aantal records in de database
            int dynamicTestValue = 10; //This is the "dynamic" value 
            MyNiceObject mObject;
            List<MyNiceObject > mObjectList = new List<MyNiceObject >();
            for (int i = 0; i < dynamicTestValue; i++)
            {
                //Ingevuld worden door een database
                mObject = new MyNiceObject ("FullNameOfSomething", "Number" + i);
                mObject.AdvancedInfo = "MyAdvancedInfo" + i;
                mObjectList.Add(mObject);

            }
            TimeLineView.ItemsSource = mObjectList;
            var template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, "FullName");
            template.SetBinding(TextCell.DetailProperty, "Number");
            //template.SetBinding(TextCell.DetailProperty, "AdvancedInfo");
            TimeLineView.ItemTemplate = template;
        }

        async void OnItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
    }
}