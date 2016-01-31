using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mapbox.vector.tile.tests
{
    [TestClass]
    public class TileParserTests
    {
        [TestMethod]
        public void TestBagVectorTile()
        {
            // arrange
            const string bagfile = "mapbox.vector.tile.tests.testdata.bag-17-67317-43082.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bagfile);
            var layerInfos = VectorTileParser.Parse(pbfStream,67317,43082,17);

            // assert
            Assert.IsTrue(layerInfos.Count==1);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 83);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);
        }

        [TestMethod]
        public void TestMapzenTileFromUrl()
        {
            // arrange
            var url = "http://vector.mapzen.com/osm/all/0/0/0.mvt";

            // Note: Use GzipWebClient with automatic decompression 
            // instead of regular WebClient otherwise we get exception 
            // 'ProtoBuf.ProtoException: Invalid wire-type; this usually means you have over-written a file without truncating or setting the length'
            var webCLient = new GZipWebClient();
            var bytes = webCLient.DownloadData(url);
            if (bytes != null)
            {
                var stream = new MemoryStream(bytes);

                // act
                var layerInfos = VectorTileParser.Parse(stream, 0, 0, 0);

                // assert
                Assert.IsTrue(layerInfos.Count > 0);
            }
        }


        [TestMethod]
        public void TestMapzenTile()
        {
            // arrange
            const string mapzenfile = "mapbox.vector.tile.tests.testdata.mapzen000.mvt";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapzenfile);
            var layerInfos = VectorTileParser.Parse(pbfStream, 0, 0, 0);

            // assert
            Assert.IsTrue(layerInfos.Count == 10);
        }


        [TestMethod]
        // tests from https://github.com/mapbox/vector-tile-js/blob/master/test/parse.test.js
        public void TestMapBoxVectorTileWithGeographicPositions()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream, 8801, 5371, 14);

            // assert
            var park = layerInfos[17].FeatureCollection.Features[11];
            var pnt = (Point)park.Geometry;
            var p = (GeographicPosition)pnt.Coordinates;
            Assert.IsTrue(Math.Abs(p.Longitude - 13.40225) < 0.0001);
            Assert.IsTrue(Math.Abs(p.Latitude - 52.54398) < 0.0001);
        }

        [TestMethod]
        // tests from https://github.com/mapbox/vector-tile-js/blob/master/test/parse.test.js
        public void TestMapBoxVectorTile()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream,8801,5371,14,false);

            // check features
            Assert.IsTrue(layerInfos.Count == 20);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 107);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count == 2);

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
            Assert.IsTrue(Math.Abs(p.Longitude - 3898) < 0.1);
            Assert.IsTrue(Math.Abs(p.Latitude - 1731) < 0.1);

            // Check line geometry from roads
            var road = layerInfos[8].FeatureCollection.Features[656];
            var ls = (LineString) road.Geometry;
            Assert.IsTrue(ls.Coordinates.Count == 3);
            var firstPoint = (GeographicPosition)ls.Coordinates[0];
            Assert.IsTrue(Math.Abs(firstPoint.Longitude - 1988) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Latitude - 306) < 0.1);

            var secondPoint = (GeographicPosition)ls.Coordinates[1];
            Assert.IsTrue(Math.Abs(secondPoint.Longitude - 1808) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Latitude - 321) < 0.1);

            var thirdPoint = (GeographicPosition)ls.Coordinates[2];
            Assert.IsTrue(Math.Abs(thirdPoint.Longitude - 1506) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Latitude - 347) < 0.1);

            // check building geometry
            var buildings = layerInfos[5].FeatureCollection.Features[0];
            var poly = ((Polygon)buildings.Geometry).Coordinates[0];
            Assert.IsTrue(poly.Coordinates.Count == 5);
            var p1 = (GeographicPosition)poly.Coordinates[0];
            Assert.IsTrue(Math.Abs(p1.Longitude - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(p1.Latitude - (-32)) < 0.1);
            var p2 = (GeographicPosition)poly.Coordinates[1];
            Assert.IsTrue(Math.Abs(p2.Longitude - 2035) < 0.1);
            Assert.IsTrue(Math.Abs(p2.Latitude - (-31)) < 0.1);
            var p3 = (GeographicPosition)poly.Coordinates[2];
            Assert.IsTrue(Math.Abs(p3.Longitude - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(p3.Latitude - (-31)) < 0.1);
            var p4 = (GeographicPosition)poly.Coordinates[3];
            Assert.IsTrue(Math.Abs(p4.Longitude - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(p4.Latitude - (-32)) < 0.1);
            var p5 = (GeographicPosition)poly.Coordinates[4];
            Assert.IsTrue(Math.Abs(p5.Longitude - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(p5.Latitude - (-32)) < 0.1);
        }
    }
}
