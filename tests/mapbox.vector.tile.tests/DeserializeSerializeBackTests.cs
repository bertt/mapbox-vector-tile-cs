using Mapbox.Vector.Tile;
using NUnit.Framework;
using ProtoBuf;
using System.IO;
using System.Reflection;

namespace mapbox.vector.tile.tests
{
    public class DeserializeSerializeBackTests
    {
        [Test]
        public void TestIssue16()
        {
            string pbf = "mapbox.vector.tile.tests.testdata.16_34440_23455_raw.mvt";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pbf);

            // deserialize the tile first
            var tile = Serializer.Deserialize<Tile>(pbfStream);
            Assert.IsTrue(tile.Layers[4].Name == "road___1");
            Assert.IsTrue(tile.Layers[4].Values[1].IntValue == 0);
            Assert.IsTrue(tile.Layers[4].Values[1].HasIntValue);

            // it is enough to serialize into stream
            var serializedTileStream = new MemoryStream();
            Serializer.Serialize<Tile>(serializedTileStream, tile);

            // read the stream again
            serializedTileStream.Seek(0, SeekOrigin.Begin);
            var deserializedTile = Serializer.Deserialize<Tile>(serializedTileStream);

            Assert.IsTrue(deserializedTile.Layers[4].Name == "road___1");
            Assert.IsTrue(deserializedTile.Layers[4].Values[1].IntValue == 0);

            // next line: 
            // expected result: true,
            // actual result: false 
            // see https://github.com/bertt/mapbox-vector-tile-cs/issues/16)
            Assert.IsTrue(deserializedTile.Layers[4].Values[1].HasIntValue);
        }
    }
}
