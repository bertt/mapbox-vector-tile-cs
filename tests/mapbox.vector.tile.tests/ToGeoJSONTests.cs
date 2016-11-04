using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Reflection;

namespace Mapbox.Vector.Tile
{
    public class ToGeoJsonTests
    {
        [Test]
        public void TestToGeoJsonPolygonFeature()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var building = layerInfos[5].VectorTileFeatures[0];

            var geoJson = building.ToGeoJSON(8801, 5371, 14);
            var json = JsonConvert.SerializeObject(geoJson);

            // act
            var actualResult = JObject.Parse(json);

            var expectedResult = JObject.Parse(@"
                {
                    type: 'Feature',
                    id: '1000267229912',
                    properties: {
                        osm_id: 1000267229912
                    },
                    geometry: {
                        type: 'Polygon',
                        coordinates: [[[13.392285704612732, 52.54974045706258], [13.392264246940613, 52.549737195107554],
                            [13.392248153686523, 52.549737195107554], [13.392248153686523, 52.54974045706258],
                            [13.392285704612732, 52.54974045706258]]]
                    }
                }
            ");

            // assert
            Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
        }

        [Test]
        public void TestToGeoJsonLineFeature()
        {
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var bridge = layerInfos[9].VectorTileFeatures[0];

            var geoJson = bridge.ToGeoJSON(8801, 5371, 14);
            var json = JsonConvert.SerializeObject(geoJson);
            var actualResult = JObject.Parse(json);

            var expectedResult = JObject.Parse(@"
                {
                    type: 'Feature',
                    id: '238162948',
                    properties: {
                        class: 'service',
                        oneway: 0,
                        osm_id: 238162948,
                        type: 'service'
                    },
                    geometry: {
                        type: 'LineString',
                        coordinates: [[13.399457931518555, 52.546334844036416], [13.399441838264465, 52.546504478525016]]
                    }
                }
                ");

                Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));

        }

        [Test]
        public void TestToGeoJsonPointFeature()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var parkFeature = layerInfos[17].VectorTileFeatures[11];

            // act
            var geoJson = parkFeature.ToGeoJSON(8801, 5371, 14);
            var json = JsonConvert.SerializeObject(geoJson);
            var actualResult = JObject.Parse(json);

            var expectedResult = JObject.Parse(@"{
                type: 'Feature',
                id: '3000003150561',
                properties:
                {
                    localrank: 1,
                    maki: 'park',
                    name: 'Mauerpark',
                    name_de: 'Mauerpark',
                    name_en: 'Mauerpark',
                    name_es: 'Mauerpark',
                    name_fr: 'Mauerpark',
                    osm_id: 3000003150561,
                    ref: '',
                    scalerank: 2,
                    type: 'Park'
                },
                geometry:
                {
                    type: 'Point',
                    coordinates: [13.402258157730103, 52.54398925380624]
                }
            }");

            // assert
            Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));
        }
    }
}
