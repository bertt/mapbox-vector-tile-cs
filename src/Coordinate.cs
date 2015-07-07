using System;
using GeoJSON.Net.Geometry;

namespace mapbox.vector.tile
{
    public class Coordinate
    {
        public long Longitude { get; set; }
        public long Latitude { get; set; }


        public GeographicPosition ToGeographicPosition(int x, int y, int z, uint extent)
        {
            var size = extent*Math.Pow(2, z);
            var x0 = extent*x;
            var y0 = extent*y;

            var y2 = 180 - (Latitude + y0)*360/size;
            var lon = (Longitude + x0)*360/size - 180;
            var lat = 360/Math.PI*Math.Atan(Math.Exp(y2*Math.PI/180)) - 90;

            var g = new GeographicPosition(lat,lon);
            return g;
        }
    }
}
