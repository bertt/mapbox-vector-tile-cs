using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using NUnit.Framework;
using static mapbox.vector.tile.Tile;
using System.Net;
using System.Net.Http;

namespace mapbox.vector.tile.tests
{
    public class TileParserTests
    {
        [Test]
        // test for issue 3 https://github.com/bertt/mapbox-vector-tile-cs/issues/3
        // tile: https://b.tiles.mapbox.com/v4/mapbox.mapbox-terrain-v2,mapbox.mapbox-streets-v7/13/4260/2911.vector.pbf
        public void TestIssue3MapBoxVectorTile()
        {
            // arrange
            const string mapboxissue3file = "mapbox.vector.tile.tests.testdata.issue3_2911.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxissue3file);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // asserts
			Assert.IsTrue(layerInfos[7].VectorTileFeatures.Count == 225);
			Assert.IsTrue(layerInfos[0].Version == 2);
			Assert.IsTrue(layerInfos[7].Name=="road");
			Assert.IsTrue(layerInfos[7].Extent == 4096);
            var firstroad = layerInfos[7].VectorTileFeatures[0];
            Assert.IsTrue(firstroad.Geometry.Count == 5);
            Assert.IsTrue(firstroad.Geometry[0].Count == 1);
            Assert.IsTrue(firstroad.Geometry[0][0].X == 816);
            Assert.IsTrue(firstroad.Geometry[0][0].Y == 3446);

            var secondroad = layerInfos[7].VectorTileFeatures[1];
            Assert.IsTrue(secondroad.Geometry.Count == 2);
            Assert.IsTrue(secondroad.Geometry[0].Count == 9);
            Assert.IsTrue(secondroad.Geometry[0][0].X == 3281);
            Assert.IsTrue(secondroad.Geometry[0][0].Y == 424);
        }

        [Test]
        public void TestBagVectorTile()
        {
            // arrange
            const string bagfile = "mapbox.vector.tile.tests.testdata.bag-17-67317-43082.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bagfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(layerInfos.Count==1);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures.Count== 83);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures[0].GeometryType == GeomType.Polygon);
        }

        [Test]
        public void TestMapzenTileFromUrl()
        {
            // arrange
            var url = "http://vector.mapzen.com/osm/all/0/0/0.mvt";

            // Note: Use HttpClient with automatic decompression 
            // instead of regular HttpClient otherwise we get exception 
            // 'ProtoBuf.ProtoException: Invalid wire-type; this usually means you have over-written a file without truncating or setting the length'
            var gzipWebClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            var bytes = gzipWebClient.GetByteArrayAsync(url).Result;

            if (bytes != null)
            {
                var stream = new MemoryStream(bytes);

                // act
                var layerInfos = VectorTileParser.Parse(stream);

                // assert
                Assert.IsTrue(layerInfos.Count > 0);
            }
        }

        [Test]
        public void TestMapzenTile()
        {
            // arrange
            const string mapzenfile = "mapbox.vector.tile.tests.testdata.mapzen000.mvt";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapzenfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(layerInfos.Count == 10);
        }

        [Test]
        // tests from https://github.com/mapbox/vector-tile-js/blob/master/test/parse.test.js
        public void TestMapBoxVectorTileWithGeographicPositions()
        {
        }

        [Test]
        public void TestMapBoxVectorTileNew()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // check features
            Assert.IsTrue(layerInfos.Count == 20);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures.Count == 107);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures[0].Attributes.Count == 2);

            // check park feature
            var park = layerInfos[17].VectorTileFeatures[11];
            var firstOrDefault = (from prop in park.Attributes where prop.Key == "name" select prop.Value).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var namePark = firstOrDefault.ToString();
                Assert.IsTrue(namePark == "Mauerpark");
            }

            // check point geometry type from park
            Assert.IsTrue(park.Id == "3000003150561");
            Assert.IsTrue(park.GeometryType == Tile.GeomType.Point);
            Assert.IsTrue(park.Geometry.Count == 1);
            Assert.IsTrue(park.Geometry[0].Count == 1);
            var p = park.Geometry[0][0];
            Assert.IsTrue(Math.Abs(p.X - 3898) < 0.1);
            Assert.IsTrue(Math.Abs(p.Y - 1731) < 0.1);

            // Check line geometry from roads
            var road = layerInfos[8].VectorTileFeatures[656];
            Assert.IsTrue(road.Id == "241452814");
            Assert.IsTrue(road.GeometryType == GeomType.LineString);
            var ls = road.Geometry;
            Assert.IsTrue(ls.Count == 1);
            Assert.IsTrue(ls[0].Count == 3);
            var firstPoint = ls[0][0];
            Assert.IsTrue(Math.Abs(firstPoint.X - 1988) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Y - 306) < 0.1);

            var secondPoint = ls[0][1];
            Assert.IsTrue(Math.Abs(secondPoint.X - 1808) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Y - 321) < 0.1);

            var thirdPoint = ls[0][2];
            Assert.IsTrue(Math.Abs(thirdPoint.X - 1506) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Y - 347) < 0.1);

            // Check polygon geometry for buildings
            var building = layerInfos[5].VectorTileFeatures[0];
            Assert.IsTrue(building.Id == "1000267229912");
            Assert.IsTrue(building.GeometryType == GeomType.Polygon);
            var b = building.Geometry;
            Assert.IsTrue(b.Count == 1);
            Assert.IsTrue(b[0].Count == 5);
            firstPoint = b[0][0];
            Assert.IsTrue(Math.Abs(firstPoint.X - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Y + 32) < 0.1);
            secondPoint = b[0][1];
            Assert.IsTrue(Math.Abs(secondPoint.X - 2035) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Y + 31) < 0.1);
            thirdPoint = b[0][2];
            Assert.IsTrue(Math.Abs(thirdPoint.X - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Y + 31) < 0.1);
            var fourthPoint = b[0][3];
            Assert.IsTrue(Math.Abs(fourthPoint.X - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(fourthPoint.Y + 32) < 0.1);
            var fifthPoint = b[0][4];
            Assert.IsTrue(Math.Abs(fifthPoint.X - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(fifthPoint.Y + 32) < 0.1);
        }

        [Test]
        // tests from https://github.com/mapbox/vector-tile-js/blob/master/test/parse.test.js
        public void TestMapBoxVectorTile()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.Parse(pbfStream);

            // check features
            Assert.IsTrue(layerInfos.Count == 20);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures.Count == 107);
            Assert.IsTrue(layerInfos[0].VectorTileFeatures[0].Attributes.Count == 2);

            // check park feature
            var park = layerInfos[17].VectorTileFeatures[11];
            var firstOrDefault = (from prop in park.Attributes where prop.Key=="name" select prop.Value).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var namePark = firstOrDefault.ToString();
                Assert.IsTrue(namePark=="Mauerpark");
            }
            var pnt = park.Geometry[0];
            var p = pnt[0];
            Assert.IsTrue(Math.Abs(p.X - 3898) < 0.1);
            Assert.IsTrue(Math.Abs(p.Y - 1731) < 0.1);

            // Check line geometry from roads
            var road = layerInfos[8].VectorTileFeatures[656];
            var ls = road.Geometry[0];
            Assert.IsTrue(ls.Count == 3);
            var firstPoint = ls[0];
            Assert.IsTrue(Math.Abs(firstPoint.X - 1988) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Y - 306) < 0.1);

            var secondPoint = ls[1];
            Assert.IsTrue(Math.Abs(secondPoint.X - 1808) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Y - 321) < 0.1);

            var thirdPoint = ls[2];
            Assert.IsTrue(Math.Abs(thirdPoint.X - 1506) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Y - 347) < 0.1);

            // check building geometry
            var buildings = layerInfos[5].VectorTileFeatures[0];
            var poly = buildings.Geometry[0];
            Assert.IsTrue(poly.Count == 5);

            var p1 = poly[0];
            Assert.IsTrue(Math.Abs(p1.X - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(p1.Y - (-32)) < 0.1);
            var p2 = poly[1];
            Assert.IsTrue(Math.Abs(p2.X - 2035) < 0.1);
            Assert.IsTrue(Math.Abs(p2.Y - (-31)) < 0.1);
            var p3 = poly[2];
            Assert.IsTrue(Math.Abs(p3.X - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(p3.Y - (-31)) < 0.1);
            var p4 = poly[3];
            Assert.IsTrue(Math.Abs(p4.X - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(p4.Y - (-32)) < 0.1);
            var p5 = poly[4];
            Assert.IsTrue(Math.Abs(p5.X - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(p5.Y - (-32)) < 0.1);
        }
    }
}
