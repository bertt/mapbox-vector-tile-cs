using System.IO;
using NUnit.Framework;
using ProtoBuf;

namespace Mapbox.Vector.Tile.tests;

public class AttributesParserTests
{
    [Test]
    public void TestAttributeParser()
    {
        // arrange
        const string mapboxfile = "14-8801-5371.vector.pbf";
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapboxfile));
        var tile = Serializer.Deserialize<Tile>(pbfStream);
        var keys = tile.Layers[0].Keys;
        var values = tile.Layers[0].Values;
        var tagsf1 = tile.Layers[0].Features[0].Tags;

        // act
        var attributes = AttributesParser.Parse(keys, values, tagsf1);

        // assert
        Assert.That(attributes.Count == 2);
        Assert.That(attributes[0].Key == "class");
        Assert.That((string)attributes[0].Value == "park");
        Assert.That(attributes[1].Key == "osm_id");
        Assert.That(attributes[1].Value.ToString() == "3000000224480");
    }
}
