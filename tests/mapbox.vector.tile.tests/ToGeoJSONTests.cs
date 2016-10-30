using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Reflection;

namespace mapbox.vector.tile.tests
{
    public class ToGeoJSONTests
    {
        [Test]
        public void TestToGeoJSONPointFeature()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var parkFeature = layerInfos[17].VectorTileFeatures[11];

            // act
            var geoJson = parkFeature.ToGeoJSON(8801, 5371, 14, layerInfos[17].Extent);
            var json = JsonConvert.SerializeObject(geoJson);
            var actualResult = JObject.Parse(json);

            // assert
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
            }}");


            Assert.IsTrue(JToken.DeepEquals(actualResult, expectedResult));


        }
    }
}
