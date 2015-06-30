using System.Linq;
using System.Reflection;
using Mapbox.Vectors.ExtensionMethods;
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
            var odds = tagsf1.GetOdds();
            var evens = tagsf1.GetEvens();

            for (var i = 0; i < evens.ToList().Count; i++)
            {
                var key = keys[(int)evens.ToList()[i]];
                var val = values[(int)odds.ToList()[i]];
                
                // todo check op the following fields in val
                // 'bool_value', 'double_value', 'float_value', 'int_value','sint_value', 'string_value', 'uint_value'

            }

            // assert
        }

    }
}
