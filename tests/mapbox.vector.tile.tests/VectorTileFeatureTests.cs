using NUnit.Framework;
using System.Reflection;

namespace mapbox.vector.tile.tests
{
    public class VectorTileFeatureTests
    {

        [Test]
        public void TestVectorTileFeatureToGeoJSON()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.ParseNew(pbfStream);
            var park = layerInfos[17].VectorTileFeatures[11];

            // assert
            //var geoJsonPark = park.ToGeoJSON(8801, 5371, 14);

            /**var pnt = (Point)park.Geometry;
            var p = (GeographicPosition)pnt.Coordinates;
            Assert.IsTrue(Math.Abs(p.Longitude - 13.40225) < 0.0001);
            Assert.IsTrue(Math.Abs(p.Latitude - 52.54398) < 0.0001);
            */


            // act

            // assert
        }
    }
}
