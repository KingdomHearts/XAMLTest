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
        public Navigation()
        {
            InitializeComponent();
            pmap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(0, 0), 360, 360));
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(pmap);
            Content = stack;
        }
        public IntPtr Handle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //FormsMaps.Init();
        async public Task<Plugin.Geolocator.Abstractions.Position> GetLocation()
        {

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(1000));
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
    }
}