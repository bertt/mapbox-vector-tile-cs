using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests;

public class ZigZagTests
{
    [Test]
    public void TestZigZagDecode()
    {
        // arrange
        const int inputVar = 1;

        // act
        var res = ZigZag.Decode(inputVar);

        // assert
        Assert.That(res == -1);
    }

    [Test]
    public void AnotherTestZigZagDecode()
    {
        // arrange
        const int inputVar = 3;

        // act
        var res = ZigZag.Decode(inputVar);

        // assert
        Assert.That(res == -2);
    }

    [Test]
    public void TestZigZagEncode()
    {
        // arrange
        const int inputVar = -2;

        // act
        var res = ZigZag.Encode(inputVar);

        // assert
        Assert.That(res == 3);
    }
}
