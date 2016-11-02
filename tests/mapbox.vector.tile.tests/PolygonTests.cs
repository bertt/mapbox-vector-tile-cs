using Mapbox.Vector.Tile;
using NUnit.Framework;
using System.Collections.Generic;

namespace mapbox.vector.tile.tests
{
    public class PolygonTests
    {
        [Test]
        public void TestCWPolygon()
        {
            // arrange
            var firstp = new Coordinate() {X=1,Y=1};
            var secondp = new Coordinate() {X = 1, Y = -1 };
            var thirdp = new Coordinate() { X = -1, Y = -1 };
            var fourthp = new Coordinate() { X = -1, Y = 1 };
            var coords = new List<Coordinate>() { firstp, secondp, thirdp, fourthp,firstp};
            var poly = new Polygon(coords);

            // act
            var ccw = poly.isCW();

            // assert
            Assert.IsTrue(ccw);
        }

        [Test]
        public void TestCCWPolygon()
        {
            // arrange
            var firstp = new Coordinate() { X = 1, Y = 1 };
            var secondp = new Coordinate() { X = -1, Y = 1 };
            var thirdp = new Coordinate() { X = -1, Y = -1 };
            var fourthp = new Coordinate() { X = 1, Y = -1 };
            var coords = new List<Coordinate>() { firstp, secondp, thirdp, fourthp, firstp };
            var poly = new Polygon(coords);

            // act
            var ccw = poly.isCCW();

            // assert
            Assert.IsTrue(ccw);
        }

    }
}
