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
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using XAMLTest.Data;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using XAMLTest.Models;

namespace XAMLTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : ContentPage, ILocationListener
    {
        Xamarin.Forms.Maps.Position position;
        LocationManager locationManager;
        /*Xamarin.Forms.Maps.Map*/ DirectionMap directionMap = new DirectionMap(); //new Xamarin.Forms.Maps.Map();
        Xamarin.Forms.GoogleMaps.Map gmap = new Xamarin.Forms.GoogleMaps.Map();
        Thread t;
        Root routedes;
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

                List<Plugin.Geolocator.Abstractions.Position> startAndEndPos = new List<Plugin.Geolocator.Abstractions.Position>();
                locator.PositionChanged += Locator_PositionChanged;
                Plugin.Geolocator.Abstractions.Position position;
                if (StartLocatie.Text == "Mijn")
                {
                    locator.DesiredAccuracy = 100;
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(3));
                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Type = Xamarin.Forms.Maps.PinType.Place,
                        Position = (new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)),
                        Label = "Start",
                        Address = "custom detail info"
                    };
                    startAndEndPos.Add(position);
                    directionMap.Pins.Add(pin);
                }
                else if (StartLocatie.Text != "" && StartLocatie.Text != null)
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

                    startAndEndPos.Add(positionadress.LastOrDefault());
                    directionMap.Pins.Add(pin);
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

                    startAndEndPos.Add(positionadress.LastOrDefault());
                    directionMap.Pins.Add(pin);
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);
                directionMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(10)));

                GetApiCallAsync(startAndEndPos);

                //var position = positionadress.FirstOrDefault();*/

                //Content = pmap;
                //directionMap.MapType = pmap.MapType;
                //directionMap.IsVisible = true;
                //stack.Children.Add(slider);
                //var stack = new StackLayout { Spacing = 0 };
                //stack.Children.Add(pmap);
                //stack.Children.Add(slider);
            }
        }

        private void GetApiCallAsync(List<Plugin.Geolocator.Abstractions.Position> positions)
        {
            string json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = wc.DownloadString("http://router.project-osrm.org/route/v1/driving/" + positions[0].Longitude + "," + positions[0].Latitude + ";" + positions[1].Longitude + "," + positions[1].Latitude + "?steps=true");
                }
                catch
                {
                    Task.Delay(1000).ContinueWith(t => GetApiCallAsync(positions));
                    return;
                }
                
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    object objectjson = JsonConvert.DeserializeObject(json);
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Root));
                    ms.Position = 0;
                    try
                    {
                        routedes = (Root)deserializer.ReadObject(ms);
                        DrawDirectionOnMap();
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                }
            }
        }   

        void DrawDirectionOnMap()
        { 
            if (directionMap.RouteCoordinates.Count != 0)
	        {
                directionMap.RouteCoordinates = new List<Xamarin.Forms.Maps.Position>();
	        }
            foreach (Step step in routedes.routes[0].legs[0].steps)
            {
                foreach (Intersection intersection in step.intersections)
                {
                    directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(intersection.location[1], intersection.location[0]));
                }
            }
            //directionMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(routedes.routes[0].legs[0].steps[0].intersections[0].location[0], routedes.routes[0].legs[0].steps[0].intersections[0].location[1]), Xamarin.Forms.Maps.Distance.FromKilometers(1.0)));
            //List<Xamarin.Forms.Maps.Position> positions = directionMap.RouteCoordinates;
            /*List<Xamarin.Forms.Maps.Position> positions = new List<Xamarin.Forms.Maps.Position>();
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.797534, -122.401827));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.797510, -122.402060));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.790269, -122.400589));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.790265, -122.400474));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.790228, -122.400391));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.790126, -122.400360));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.789250, -122.401451));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.788440, -122.400396));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.787999, -122.399780));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.786736, -122.398202));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.786345, -122.397722));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.785983, -122.397295));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.785559, -122.396728));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.780624, -122.390541));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.777113, -122.394983));
            directionMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(37.776831, -122.394627));

            directionMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(37.79752, -122.40183), Xamarin.Forms.Maps.Distance.FromMiles(1.0)));
            */
            //DirectionMap dm = new DirectionMap();
            //dm = directionMap;
            //directionMap = dm;

            Content = directionMap;
            directionMap.IsVisible = true;
            //Content = directionMap;
            string test = "";
        }
        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            directionMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(10)));

        }
    }
}