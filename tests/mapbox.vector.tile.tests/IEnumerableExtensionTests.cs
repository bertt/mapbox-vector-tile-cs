using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests;

public class EnumerableExtensionTests
{
    [Test]
    public void TestEvensMethod()
    {
        // arrange
        var sequence = new List<int> { 0, 1, 2, 3 };

        // act
        var evens = sequence.GetEvens().ToList();

        // assert
        Assert.That(evens[0] == 0);
        Assert.That(evens[1] == 2);
    }

    [Test]
    public void TestOddsMethod()
    {
        // arrange
        var sequence = new List<int> { 0, 1, 2, 3 };

        // act
        var evens = sequence.GetOdds().ToList();

        // assert
        Assert.That(evens[0] == 1);
        Assert.That(evens[1] == 3);
    }
}
