using Mapbox.Vector.Tile.tests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests;

public class SingletonGeometryTests
{
    [Test]
    public void SingletonPointTest()
    {
        // arrange
        const string mapboxfile = "mapbox.vector.tile.tests.testdata.singleton-point.pbf";
        var feature = PbfLoader.GeoJSONFromFixture(mapboxfile);

        // act
        var json = JsonConvert.SerializeObject(feature);
        var actualResult = JObject.Parse(json);
        var expectedResult = JObject.Parse(@"
            {
              'geometry': {
                'coordinates': [
                  0.966796875,
                  2.0210651187669839
                ],
                'type': 'Point'
              },
              'id': '1',
              'properties': {},
              'type': 'Feature'
            }
            ");
        // assert
        Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
    }

    [Test]
    public void SingletonLineTest()
    {
        // arrange
        const string mapboxfile = "mapbox.vector.tile.tests.testdata.singleton-line.pbf";
        var feature = PbfLoader.GeoJSONFromFixture(mapboxfile);

        // act
        var json = JsonConvert.SerializeObject(feature);
        var actualResult = JObject.Parse(json);
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
                    'type': 'LineString'
                  },
                  'id': '1',
                  'properties': {},
                  'type': 'Feature'
                }            
            ");
        // assert
        Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
    }


    [Test]
    public void SingletonPolygonTest()
    {
        // arrange
        const string mapboxfile = "mapbox.vector.tile.tests.testdata.singleton-polygon.pbf";
        var feature = PbfLoader.GeoJSONFromFixture(mapboxfile);

        // act
        var json = JsonConvert.SerializeObject(feature);
        var actualResult = JObject.Parse(json);
        var expectedResult = JObject.Parse(@"
                {
                  'geometry': {
                    'coordinates': [
                      [
                        [
                          0.966796875,
                          0.0
                        ],
                        [
                          0.0,
                          0.0
                        ],
                        [
                          0.966796875,
                          0.96675099976664569
                        ],
                        [
                          0.966796875,
                          0.0
                        ]
                      ]
                    ],
                    'type': 'Polygon'
                  },
                  'id': '1',
                  'properties': {},
                  'type': 'Feature'
                }
            ");
        // assert
        Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
    }


}
