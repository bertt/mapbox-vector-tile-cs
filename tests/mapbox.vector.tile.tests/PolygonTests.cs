using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests;

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
        Assert.That(poly.SignedArea() < 0);
        Assert.That(ccw);
    }

    [Test]
    public void TestCCWPolygon()
    {
        var coords = TestData.GetCCWPolygon(1);
        var poly = new VTPolygon(coords);

        // act
        var ccw = poly.IsCCW();

        // assert
        Assert.That(poly.SignedArea() > 0);
        Assert.That(ccw);
    }
}
