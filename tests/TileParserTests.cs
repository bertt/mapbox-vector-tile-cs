using System.Linq;
using System.Reflection;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mapbox.Vectors.Tests
{
    [TestClass]
    public class TileParserTests
    {
        // tests from https://github.com/mapbox/vector-tile-js/blob/master/test/parse.test.js

        [TestMethod]
        public void TestBagVecorTile()
        {
            // arrange
            const string bagfile = "Mapbox.Vectors.testdata.bag.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bagfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(layerInfos.Count==1);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 47);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);

        }

        [TestMethod]
        public void TestMapBoxVectorTile()
        {
            // arrange
            const string mapboxfile = "Mapbox.Vectors.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(layerInfos.Count == 20);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 107);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count==2);
        }

        [TestMethod]
        public void TestAnotherMapBoxVectorTile()
        {
            // arrange
            const string mapboxfile1 = "Mapbox.Vectors.testdata.96.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile1);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(layerInfos.Count == 11);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count==256);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count == 1);
        }

        [TestMethod]
        public void TestMapBoxDecodeTest()
        {
            // arrange
            const string mapboxfile = "Mapbox.Vectors.testdata.14-8801-5371.vector1.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // check park feature
            var park = layerInfos[17].FeatureCollection.Features[11];
            var firstOrDefault = (from prop in park.Properties where prop.Key=="name" select prop.Value).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var namePark = firstOrDefault.ToString();
                Assert.IsTrue(namePark=="Mauerpark");
            }
            var pnt = (Point)park.Geometry;
            var p = (GeographicPosition)pnt.Coordinates;
            Assert.IsTrue(p.Longitude == 3898);
            Assert.IsTrue(p.Latitude == 1731);

            // Check line geometry from roads
            var road = layerInfos[8].FeatureCollection.Features[656];
            var ls = (LineString) road.Geometry;
            Assert.IsTrue(ls.Coordinates.Count == 3);
            var firstPoint = (GeographicPosition)ls.Coordinates[0];
            Assert.IsTrue(firstPoint.Longitude==1988);
            Assert.IsTrue(firstPoint.Latitude == 306);

            var secondPoint = (GeographicPosition)ls.Coordinates[1];
            Assert.IsTrue(secondPoint.Longitude == 1808);
            Assert.IsTrue(secondPoint.Latitude == 321);

            var thirdPoint = (GeographicPosition)ls.Coordinates[2];
            Assert.IsTrue(thirdPoint.Longitude == 1506);
            Assert.IsTrue(thirdPoint.Latitude == 347);

            // check building geometry
            var buildings = layerInfos[5].FeatureCollection.Features[0];
            var poly = ((Polygon)buildings.Geometry).Coordinates[0];
            Assert.IsTrue(poly.Coordinates.Count == 5);
            var p1 = (GeographicPosition)poly.Coordinates[0];
            Assert.IsTrue(p1.Longitude == 2039);
            Assert.IsTrue(p1.Latitude == -32);
            var p2 = (GeographicPosition)poly.Coordinates[1];
            Assert.IsTrue(p2.Longitude == 2035);
            Assert.IsTrue(p2.Latitude == -31);
            var p3 = (GeographicPosition)poly.Coordinates[2];
            Assert.IsTrue(p3.Longitude == 2032);
            Assert.IsTrue(p3.Latitude == -31);
            var p4 = (GeographicPosition)poly.Coordinates[3];
            Assert.IsTrue(p4.Longitude == 2032);
            Assert.IsTrue(p4.Latitude == -32);
            var p5 = (GeographicPosition)poly.Coordinates[4];
            Assert.IsTrue(p5.Longitude == 2039);
            Assert.IsTrue(p5.Latitude == -32);
        }
    }
}
