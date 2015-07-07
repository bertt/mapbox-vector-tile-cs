using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;

namespace mapbox.vector.tile.tests
{
    [TestClass]
    public class AttributesParserTests
    {
        [TestMethod]
        public void TestAttributeParser()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var tile = Serializer.Deserialize<Tile>(pbfStream);
            var keys = tile.Layers[0].Keys;
            var values = tile.Layers[0].Values;
            var tagsf1 = tile.Layers[0].Features[0].Tags;

            // act
            var attributes = AttributesParser.Parse(keys, values, tagsf1);

            // assert
            Assert.IsTrue(attributes.Count == 2);
            Assert.IsTrue(attributes[0].Key=="class");
            Assert.IsTrue((string)attributes[0].Value == "park");
            Assert.IsTrue(attributes[1].Key =="osm_id");
            Assert.IsTrue(attributes[1].Value.ToString() == "3000000224480");
        }
    }
}
