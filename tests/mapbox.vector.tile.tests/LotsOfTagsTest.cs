using NUnit.Framework;
using System.Reflection;

namespace Mapbox.Vector.Tile.tests;

public class LotsOfTagsTest
{
    [Test]
    public void TestLotsOfTags()
    {
        // arrange
        const string mapboxfile = "mapbox.vector.tile.tests.testdata.lots-of-tags.vector.pbf";
        var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);

        // act
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // assert
        Assert.IsTrue(layerInfos[0]!=null);
    }
}
