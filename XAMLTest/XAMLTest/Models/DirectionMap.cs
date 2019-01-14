using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace XAMLTest.Models
{
    public class DirectionMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public DirectionMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}
