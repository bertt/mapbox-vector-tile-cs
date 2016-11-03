using Mapbox.Vector.Tile;
using NUnit.Framework;

namespace mapbox.vector.tile.tests
{
    public class PolygonTests
    {

        [Test]
        public void TestCWPolygon()
        {
            // arrange
            var coords = TestData.GetCWPolygon(1);
            var poly = new VTPolygon(coords);

            // act
            var ccw = poly.IsCW();

            // assert
            Assert.IsTrue(poly.SignedArea() < 0);
            Assert.IsTrue(ccw);
        }

        [Test]
        public void TestCCWPolygon()
        {
            var coords = TestData.GetCCWPolygon(1);
            var poly = new VTPolygon(coords);

            // act
            var ccw = poly.IsCCW();

            // assert
            Assert.IsTrue(poly.SignedArea() > 0);
            Assert.IsTrue(ccw);
        }
    }
}
