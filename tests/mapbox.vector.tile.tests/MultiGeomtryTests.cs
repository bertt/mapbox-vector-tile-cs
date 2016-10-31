using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Reflection;
using mapbox.vector.tile.ExtensionMethods;

namespace mapbox.vector.tile.tests
{
    public class MultiGeomtryTests
    {
        private Feature GeoJSONFromFixture(string name)
        {
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var test = layerInfos[0].VectorTileFeatures[0].ToGeoJSON(0, 0, 0);
            return test;
        }

        [Test]
        public void TestToGeoJsonMultiPointFeature()
        {
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.multi-point.pbf";
            var feature = GeoJSONFromFixture(mapboxfile);
            var json = JsonConvert.SerializeObject(feature);
            var actualResult = JObject.Parse(json);

            // todo: why is there a difference of 0.03 wrt the mapbox js test?
            var expectedResult = JObject.Parse(@"
                {
                   'geometry': {
                    'coordinates': [
                      [
                        0.966796875,
                        2.0210651187669839
                      ],
                      [
                        2.98828125,
                        4.0396178267684348
                      ]
                    ],
                    'type': 'MultiPoint'
                  },
                  'id': '1',
                  'properties': {},
                  'type': 'Feature'
                }      
            ");
            Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
        }

    }
}
