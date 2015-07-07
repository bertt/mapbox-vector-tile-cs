using System;

namespace Mapbox.Vectors
{
    public class ScreenPixelToLatLon
    {
        public static Coordinate Convert(int extent, long xtile, long ytile, int z)
        {
            var size = extent*Math.Pow(2, z);
            var x0 = extent*xtile;
            var y0 = extent*ytile;

            // todo: add more code from 
            return null;
        }
    }
}

