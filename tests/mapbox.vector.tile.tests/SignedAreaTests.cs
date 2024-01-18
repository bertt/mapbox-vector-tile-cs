using NUnit.Framework;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile.tests;

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
        Assert.That(area == -1);
    }

    [Test]
    public void SignedAreaTest1()
    {
        // arrange
        // create a closed polygon (first point is the same as the last)
        var points = new List<Coordinate>();
        points.Add(new Coordinate() { X = -3, Y = -2 });
        points.Add(new Coordinate() { X = -1, Y = 4 });
        points.Add(new Coordinate() { X = 6, Y = 1 });
        points.Add(new Coordinate() { X = 3, Y = 10 });
        points.Add(new Coordinate() { X = -4, Y = 9 });
        points.Add(new Coordinate() { X = -3, Y = -2 });

        var polygon = new VTPolygon(points);

        // act
        var area = polygon.SignedArea();

        // assert
        // polygon is defined clock-wise so area should be negative
        Assert.That(area == 60);
    }

}