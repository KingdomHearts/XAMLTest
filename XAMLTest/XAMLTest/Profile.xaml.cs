using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAMLTest.Data;
using Xamarin.Forms;

namespace XAMLTest
{
    public partial class Profile : ContentPage
    {
        string name;
        public Profile()
        {
            InitializeComponent();
            MockData data = new MockData();
            ModelsProfile thisProfile = data.GetProfileData(User.UserName);
            //Get info of the profile
            //ProfileAchievements.
            ProfileBiography.Text = thisProfile.Bio;
            ProfileName.Text = thisProfile.FirstName + " " + thisProfile.Name;
            ProfileFriendsAmount.Text = "Vrienden - " + thisProfile.Friends.Count.ToString();
            ProfileFriends.Text = thisProfile.Friends[0];
            name = thisProfile.Friends[0];
            ProfileGroups.Text = thisProfile.Groups[0] + " " + thisProfile.Groups[1] + " " + thisProfile.Groups[2];
            //data.EditXmlProfileData("Gameshops", "XXXEroticaShop");
        }
        async void OnPreviousFriend(object sender, EventArgs e)
        {
            MockData data = new MockData();
            ModelsProfile thisProfile = data.GetProfileData(User.UserName);
            int index = thisProfile.Friends.IndexOf(name);
            if ((index -1) >= 0)
            {
                ProfileFriends.Text = thisProfile.Friends[index-1];
                name = thisProfile.Friends[index - 1];
            }
            else
            {
                ProfileFriends.Text = thisProfile.Friends[thisProfile.Friends.Count-1];
                name = thisProfile.Friends[thisProfile.Friends.Count - 1];
            }
        }
        async void OnNextFriend(object sender, EventArgs e)
        {
            MockData data = new MockData();
            ModelsProfile thisProfile = data.GetProfileData(User.UserName);
            int index = thisProfile.Friends.IndexOf(name);
            if ((index + 1) != thisProfile.Friends.Count)
            {
                ProfileFriends.Text = thisProfile.Friends[index+1];
                name = thisProfile.Friends[index + 1];
            }
            else
            {
                ProfileFriends.Text = thisProfile.Friends[0];
                name = thisProfile.Friends[0];
            }
        }

       // async void AddFriendView(object sender, EventArgs e)
        //{
            //popupName.IsVisible = true;
            //await Navigation.PushAsync(new Profile());
        //}
        async void AddFriendView(object sender, EventArgs e)
        {
            //popupName.IsVisible = true;

            MockData data = new MockData();
            data.EditXmlProfileData("Friends", "Henk Pestra");
            ModelsProfile thisProfile = data.GetProfileData(User.UserName);
            ProfileFriendsAmount.Text = "Vrienden - " + thisProfile.Friends.Count.ToString();
            //popupName.IsVisible = false;
            //await Navigation.PushAsync(new Profile());
        }
    }
}