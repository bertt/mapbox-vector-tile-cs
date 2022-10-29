using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Reflection;

namespace Mapbox.Vector.Tile.tests;

public class VectorTileLayerTests
{
    [Test]
    public void TestVectorTileToGeoJson()
    {
        // arrange
        const string name = "mapbox.vector.tile.tests.testdata.multi-point.pbf";
        var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        var layerInfos = VectorTileParser.Parse(pbfStream);
        var geojson = layerInfos[0].ToGeoJSON(0,0,0);

        // act
        var json = JsonConvert.SerializeObject(geojson);

        // act
        var actualResult = JObject.Parse(json);
        var expectedResult = JObject.Parse(@"
                {
                  'features': [
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
                  ],
                  'type': 'FeatureCollection'
                }            
            ");

        // assert
        Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
    }
}
