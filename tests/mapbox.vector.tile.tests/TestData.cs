using System.Collections.Generic;

namespace Mapbox.Vector.Tile.tests
{
    public class TestData
    {
        public static List<Coordinate> GetCWPolygon(int factor)
        {
            var firstp = new Coordinate() { X = factor, Y = factor };
            var secondp = new Coordinate() { X = factor, Y = -factor };
            var thirdp = new Coordinate() { X = -factor, Y = -factor };
            var fourthp = new Coordinate() { X = -factor, Y = factor };
            var coords = new List<Coordinate>() { firstp, secondp, thirdp, fourthp, firstp };
            return coords;
        }

        public static List<Coordinate> GetCCWPolygon(int factor)
        {
            // arrange
            var firstp = new Coordinate() { X = factor, Y = factor };
            var secondp = new Coordinate() { X = -factor, Y = factor };
            var thirdp = new Coordinate() { X = -factor, Y = -factor };
            var fourthp = new Coordinate() { X = factor, Y = -factor };
            var coords = new List<Coordinate>() { firstp, secondp, thirdp, fourthp, firstp };
            return coords;
        }
    }
}
