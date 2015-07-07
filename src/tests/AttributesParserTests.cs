using System.Reflection;
using Mapbox.Vectors.mapnik.vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;

namespace Mapbox.Vectors.tests
{
    [TestClass]
    public class AttributesParserTests
    {
        [TestMethod]
        public void TestAttributeParser()
        {
            // arrange
            const string mapboxfile = "Mapbox.Vectors.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var tile = Serializer.Deserialize<tile>(pbfStream);
            var keys = tile.layers[0].keys;
            var values = tile.layers[0].values;
            var tagsf1 = tile.layers[0].features[0].tags;

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
