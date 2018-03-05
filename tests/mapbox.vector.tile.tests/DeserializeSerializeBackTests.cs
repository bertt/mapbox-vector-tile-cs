using Mapbox.Vector.Tile;
using NUnit.Framework;
using ProtoBuf;
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

            // todo: serialize the tile (to file or something)

            // todo: read the file again

            // todo: check again the tile.Layers[4].Values[1].HasIntValue
            // expected result: true, actual result: false (according to https://github.com/bertt/mapbox-vector-tile-cs/issues/16)

        }
    }
}
