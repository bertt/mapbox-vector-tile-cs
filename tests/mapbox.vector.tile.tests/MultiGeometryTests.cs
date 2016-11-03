using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Mapbox.Vector.Tile.tests
{
    public class MultiGeometryTests
    {
        [Test]
        public void StackedMultipolygonTest()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.stacked-multipolygon.pbf";
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
                      [
                        2.021484375,
                        -2.0210651187669839
                      ],
                      [
                        -2.021484375,
                        -2.0210651187669839
                      ],
                      [
                        -2.021484375,
                        2.0210651187669839
                      ],
                      [
                        2.021484375,
                        2.0210651187669839
                      ],
                      [
                        2.021484375,
                        -2.0210651187669839
                      ]
                    ]
                  ],
                  [
                    [
                      [
                        0.966796875,
                        -0.96675099976663148
                      ],
                      [
                        -0.966796875,
                        -0.96675099976663148
                      ],
                      [
                        -0.966796875,
                        0.96675099976664569
                      ],
                      [
                        0.966796875,
                        0.96675099976664569
                      ],
                      [
                        0.966796875,
                        -0.96675099976663148
                      ]
                    ]
                  ]
                ],
                'type': 'MultiPolygon'
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
        public void TestToGeoJsonPolygonWithInnerRing()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.polygon-with-inner.pbf";
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
          2.021484375,
          -2.0210651187669839
        ],
        [
          -2.021484375,
          -2.0210651187669839
        ],
        [
          -2.021484375,
          2.0210651187669839
        ],
        [
          2.021484375,
          2.0210651187669839
        ],
        [
          2.021484375,
          -2.0210651187669839
        ]
      ],
      [
        [
          -0.966796875,
          0.96675099976664569
        ],
        [
          -0.966796875,
          -0.96675099976663148
        ],
        [
          0.966796875,
          -0.96675099976663148
        ],
        [
          0.966796875,
          0.96675099976664569
        ],
        [
          -0.966796875,
          0.96675099976664569
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


        [Test]
        public void TestToGeoJsonMultiPolygonFeature()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.multi-polygon.pbf";
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
                      [
                        [
                          [
                            -0.966796875,
                            -0.96675099976663148
                          ],
                          [
                            -0.966796875,
                            0.0
                          ],
                          [
                            0.0,
                            0.0
                          ],
                          [
                            -0.966796875,
                            -0.96675099976663148
                          ]
                        ]
                      ]
                    ],
                    'type': 'MultiPolygon'
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
        public void TestToGeoJsonMultiLineFeature()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.multi-line.pbf";
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
                          2.0210651187669839
                        ],
                        [
                          2.98828125,
                          4.0396178267684348
                        ]
                      ],
                      [
                        [
                          5.009765625,
                          5.9657536710655421
                        ],
                        [
                          7.03125,
                          7.97219771438688
                        ]
                      ]
                    ],
                    'type': 'MultiLineString'
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
        public void TestToGeoJsonMultiPointFeature()
        {
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.multi-point.pbf";
            var feature = PbfLoader.GeoJSONFromFixture(mapboxfile);
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
