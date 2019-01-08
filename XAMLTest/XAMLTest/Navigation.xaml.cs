using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using System.Threading;

namespace XAMLTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : ContentPage , ILocationListener
    {
        Xamarin.Forms.Maps.Position position;
        LocationManager locationManager;
        Xamarin.Forms.Maps.Map pmap = new Xamarin.Forms.Maps.Map();
        Xamarin.Forms.GoogleMaps.Map gmap = new Xamarin.Forms.GoogleMaps.Map();
        Thread t;
        public Navigation()
        {
            InitializeComponent();

            


            // pmap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(0, 0), 360, 360));

        }

        /*public async Task ReloadPageAsync()
        {
            await GetLocation();
            pmap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(10)));
           
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(pmap);
            Content = stack;
        }*/
        public IntPtr Handle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //FormsMaps.Init();
        //public Task<IEnumerable<Plugin.Geolocator.Abstractions.Position>> GetLocation()
        public Task<Plugin.Geolocator.Abstractions.Position> GetLocation()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 120;
            if (locator.IsGeolocationAvailable)
            {
                var test = 1;
            }
            if (locator.IsGeolocationEnabled)
            {
                var test = 1;
            }
            if (!locator.IsListening)
            {
                locator.StartListeningAsync(TimeSpan.FromSeconds(10), 1);
            }
            var position = locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            //var position = locator.GetPositionsForAddressAsync("Enschede");
            return position;
        }

        public void OnLocationChanged(Location location)
        {
            throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                var locator = CrossGeolocator.Current;
                string start = "";
                string eind = "";
                Plugin.Geolocator.Abstractions.Position position;
                if (StartLocatie.Text != "" && StartLocatie.Text != null)
                {
                    start = StartLocatie.Text;
                    var positionadress = await locator.GetPositionsForAddressAsync(start);
                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Type = Xamarin.Forms.Maps.PinType.Place,
                        Position = (new Xamarin.Forms.Maps.Position(positionadress.FirstOrDefault().Latitude, positionadress.FirstOrDefault().Longitude)),
                        Label = "Start",
                        Address = start
                    };
                    pmap.Pins.Add(pin);
                }
                else if (StartLocatie.Text == "Mijn")
                {

                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);
                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Type = Xamarin.Forms.Maps.PinType.Place,
                        Position = (new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)),
                        Label = "Start",
                        Address = "custom detail info"
                    };

                    pmap.Pins.Add(pin);
                }
                if (EindLocatie.Text != "" && EindLocatie.Text != null)
                {

                    eind = EindLocatie.Text;
                    var positionadress = await locator.GetPositionsForAddressAsync(eind);
                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Type = Xamarin.Forms.Maps.PinType.Place,
                        Position = (new Xamarin.Forms.Maps.Position(positionadress.FirstOrDefault().Latitude, positionadress.FirstOrDefault().Longitude)),
                        Label = "Eind",
                        Address = eind
                    };

                    pmap.Pins.Add(pin);
                }
                //var position = positionadress.FirstOrDefault();*/
                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);
                pmap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(10)));

                var stack = new StackLayout { Spacing = 0 };
                stack.Children.Add(pmap);
                stack.Children.Add(slider);
                Content = stack;
            }
        }
    }
}