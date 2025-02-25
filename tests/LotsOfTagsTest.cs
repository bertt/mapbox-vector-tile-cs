using NUnit.Framework;
using System.IO;

namespace Mapbox.Vector.Tile.tests;

public class LotsOfTagsTest
{
    [Test]
    public void TestLotsOfTags()
    {
        // arrange
        const string mapboxfile = "lots-of-tags.vector.pbf";
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapboxfile));

        // act
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // assert
        Assert.That(layerInfos[0] != null);
    }
}
