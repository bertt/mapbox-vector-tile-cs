using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests;

public class GeometryParserTests
{
    [Test]
    public void AnotherGeometryParserTest()
    {
        var input = new List<uint> { 9, 7796, 3462 };
        var output = GeometryParser.ParseGeometry(input, Tile.GeomType.Point);
        Assert.That(output.ToList().Count == 1);
        Assert.That(output.ToList()[0][0].X == 3898);
        Assert.That(output.ToList()[0][0].Y == 1731);
    }
}
