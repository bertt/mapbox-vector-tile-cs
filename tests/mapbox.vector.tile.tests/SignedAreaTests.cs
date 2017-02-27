using Mapbox.Vector.Tile;
using NUnit.Framework;
using System.Collections.Generic;

namespace mapbox.vector.tile.tests
{
    public class SignedAreaTests
    {
        [Test]
        public void SignedAreaTest()
        {
            // arrange
            // create a closed polygon (first point is the same as the last)
            var points = new List<Coordinate>();
            points.Add(new Coordinate() { X = 1, Y = 1 });
            points.Add(new Coordinate() { X = 2, Y = 2 });
            points.Add(new Coordinate() { X = 3, Y = 1 });
            points.Add(new Coordinate() { X = 1, Y = 1 });

            var polygon = new VTPolygon(points);

            // act
            var area = polygon.SignedArea();

            // assert
            // polygon is defined clock-wise so area should be negative
            Assert.IsTrue(area == -1);
        }
    }
}