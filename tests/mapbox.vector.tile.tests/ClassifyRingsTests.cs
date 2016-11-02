using Mapbox.Vector.Tile;
using NUnit.Framework;
using System.Collections.Generic;

namespace mapbox.vector.tile.tests
{
    public class ClassifyRingsTests
    {
        [Test]
        public void TestClassifyRings()
        {
            // arrange
            var coords = new List<List<Coordinate>>();
            var poly1 = TestData.GetCWPolygon(2);
            var poly2 = TestData.GetCWPolygon(1);
            coords.Add(poly1);
            coords.Add(poly2);

            // act
            var classify = ClassifyRings.Classify(coords);

            // assert
            Assert.IsTrue(classify.Count == 2);
        }

    }
}
