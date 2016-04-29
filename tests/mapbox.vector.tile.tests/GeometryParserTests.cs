using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace mapbox.vector.tile.tests
{
    public class GeometryParserTests
    {
        [Test]
        public void AnotherGeometryParserTest2()
        {
            var input = new List<uint> {9, 7796, 3462};
            var output = GeometryParser.ParseGeometry(input, Tile.GeomType.Point);
            Assert.IsTrue(output.ToList().Count == 1);
            Assert.IsTrue(output[0].Longitude == 3898);
            Assert.IsTrue(output[0].Latitude == 1731);
        }
    }
}
