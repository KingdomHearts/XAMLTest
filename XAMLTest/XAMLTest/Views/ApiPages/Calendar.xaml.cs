using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Android.Provider;
using Android.Content;
using Android.Database;
using Android.Widget;
using Android;
using Android.App;
using Syncfusion.SfCalendar.XForms;
using Google.Apis.Calendar.v3;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using Syncfusion.SfSchedule.XForms;
using XAMLTest.Data;

namespace XAMLTest.Views.ApiPages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage, IGoogleAuthenticationDelegate
    {
        CalendarView _calendarView;
        StackLayout _stacker;
        public static GoogleAuthenticator Auth;
        SfCalendar calendar;
        SfSchedule schedule;
        Color defaultColor = Color.Cyan;
        ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();
        public Calendar()
        {
            this.InitializeComponent();
            calendar = new SfCalendar();
            calendar.ShowInlineEvents = true;

            schedule = new SfSchedule();
            schedule.ScheduleView = ScheduleView.MonthView;

            schedule.CellLongPressed += Schedule_CellLongPressed;
            schedule.CellDoubleTapped += Schedule_CellDoubleTapped;

            ScheduleAppointmentCollection calendarObject = CalendarMockData.GetJsonFileCalendar();
            if (calendarObject != null)
            {
                for (int i = 0; i < calendarObject.Count; i++)
                {
                    calendarObject[i].Color = defaultColor;
                    scheduleAppointmentCollection.Add(calendarObject[i]);
                }
            }
            schedule.DataSource = calendarObject;

            this.Content = schedule;

           // Go();
            /*
            InitializeComponent();
            Title = "Calendar Sample";

            _stacker = new StackLayout();
            Content = _stacker;

            _calendarView = new CalendarView()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            _stacker.Children.Add(_calendarView);
            _calendarView.DateSelected += (object sender, DateTime e) = &gt;
            {
                _stacker.Children.Add(new Label()
                {
                    Text = "Date Was Selected" + e.ToString("d"),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                });
            };
        
    */
        }

        private void Schedule_CellDoubleTapped(object sender, CellTappedEventArgs e)
        {
            schedule.ScheduleView = ScheduleView.DayView;
        }

        public void Schedule_CellLongPressed(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
           // DatePickerStart.Date = System.DateTime.Today;
            //DatePickerEnd.Date = System.DateTime.Today;
            this.Content = overlay;
        }

        public void CreateAppointment(object sender, EventArgs e)
        {
            
            
            DateTime dateStart = DatePickerStart.Date;
            TimeSpan timePickerStart = TimePickerStart.Time;
            
            DateTime dateEnd = DatePickerEnd.Date;
            TimeSpan timePickerEnd = TimePickerEnd.Time;

            DateTime dateTimeStart = dateStart.Date + timePickerStart;
            DateTime dateTimeEnd = dateEnd.Date + timePickerEnd;

            string eventName = EventName.Text;
            string location = EventLocation.Text;


            scheduleAppointmentCollection.Add(new ScheduleAppointment()
            {
                StartTime = dateTimeStart,
                EndTime = dateTimeEnd,
                Subject = EventName.Text,
                Location = EventLocation.Text,
                Color = new Color(0,0,1),
            });

            schedule.DataSource = scheduleAppointmentCollection;

            CalendarMockData.CreateJsonFile(scheduleAppointmentCollection);

            overlay.IsVisible = false;
            this.Content = schedule;

            /*
            CalendarInlineEvent calendarInlineEvent = new CalendarInlineEvent();
            string eventname = EventName.Text;
            string eventLocation = EventLocation.Text;
            string startTimeString = EventStartTime.Text;
            string endTimeString = EventEndTime.Text;

            DateTime startTime = Convert.ToDateTime(startTimeString);
            DateTime endTime = Convert.ToDateTime(endTimeString);

            calendarInlineEvent.Subject = eventname;
            calendarInlineEvent.StartTime = startTime;
            calendarInlineEvent.EndTime = endTime;
            calendarInlineEvent.
            */
        }

        public void AddEvent(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
        }

        public void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.IsUsingNativeUI; //.GetUI(this);
            var intent2 = new Intent(Intent.ActionSend);
           // Xamarin.Auth.OAuth2Authenticator.g
           // StartActivity(intent);
        }
        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            
            // Retrieve the user's email address
            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);

            // Display it on the UI
            //var googleButton = FindViewById<Button>(Resource.Id.googleLoginButton);
            //googleButton.Text = $"Connected with {email}";
            
        }

        public void OnAuthenticationCanceled()
        {
            
            //new AlertDialog.Builder(this)
            //               .SetTitle("Authentication canceled")
            //               .SetMessage("You didn't completed the authentication process")
           //                .Show();
                           
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            /*
            new AlertDialog.Builder(this)
                           .SetTitle(message)
                           .SetMessage(exception?.ToString())
                           .Show();
                           */
        }

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public void Go()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("XAMLTest.credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
        }
    }
}