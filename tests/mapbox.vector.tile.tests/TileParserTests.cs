using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using NUnit.Framework;
using static mapbox.vector.tile.Tile;

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
            var layerInfos = VectorTileParser.ParseNew(pbfStream);
			Assert.IsTrue(layerInfos[7].VectorTileFeatures.Count == 225);
			Assert.IsTrue(layerInfos[0].Version == 2);
			Assert.IsTrue(layerInfos[7].Name=="road");
			Assert.IsTrue(layerInfos[7].Extent == 4096);
            var firstroad = layerInfos[7].VectorTileFeatures[0];
            Assert.IsTrue(firstroad.Geometry.Count == 5);
            Assert.IsTrue(firstroad.Geometry[0].Count == 1);
            Assert.IsTrue(firstroad.Geometry[0][0].Longitude == 816);
            Assert.IsTrue(firstroad.Geometry[0][0].Latitude == 3446);
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void TestMapBoxVectorTileNew()
        {
            // arrange
            const string mapboxfile = "mapbox.vector.tile.tests.testdata.14-8801-5371.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxfile);
            var layerInfos = VectorTileParser.ParseNew(pbfStream);

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
            Assert.IsTrue(Math.Abs(p.Longitude - 3898) < 0.1);
            Assert.IsTrue(Math.Abs(p.Latitude - 1731) < 0.1);

            // Check line geometry from roads
            var road = layerInfos[8].VectorTileFeatures[656];
            Assert.IsTrue(road.Id == "241452814");
            Assert.IsTrue(road.GeometryType == GeomType.LineString);
            var ls = road.Geometry;
            Assert.IsTrue(ls.Count == 1);
            Assert.IsTrue(ls[0].Count == 3);
            var firstPoint = ls[0][0];
            Assert.IsTrue(Math.Abs(firstPoint.Longitude - 1988) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Latitude - 306) < 0.1);

            var secondPoint = ls[0][1];
            Assert.IsTrue(Math.Abs(secondPoint.Longitude - 1808) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Latitude - 321) < 0.1);

            var thirdPoint = ls[0][2];
            Assert.IsTrue(Math.Abs(thirdPoint.Longitude - 1506) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Latitude - 347) < 0.1);

            // Check polygon geometry for buildings
            var building = layerInfos[5].VectorTileFeatures[0];
            Assert.IsTrue(building.Id == "1000267229912");
            Assert.IsTrue(building.GeometryType == GeomType.Polygon);
            var b = building.Geometry;
            Assert.IsTrue(b.Count == 1);
            Assert.IsTrue(b[0].Count == 5);
            firstPoint = b[0][0];
            Assert.IsTrue(Math.Abs(firstPoint.Longitude - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(firstPoint.Latitude + 32) < 0.1);
            secondPoint = b[0][1];
            Assert.IsTrue(Math.Abs(secondPoint.Longitude - 2035) < 0.1);
            Assert.IsTrue(Math.Abs(secondPoint.Latitude + 31) < 0.1);
            thirdPoint = b[0][2];
            Assert.IsTrue(Math.Abs(thirdPoint.Longitude - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(thirdPoint.Latitude + 31) < 0.1);
            var fourthPoint = b[0][3];
            Assert.IsTrue(Math.Abs(fourthPoint.Longitude - 2032) < 0.1);
            Assert.IsTrue(Math.Abs(fourthPoint.Latitude + 32) < 0.1);
            var fifthPoint = b[0][4];
            Assert.IsTrue(Math.Abs(fifthPoint.Longitude - 2039) < 0.1);
            Assert.IsTrue(Math.Abs(fifthPoint.Latitude + 32) < 0.1);
        }

        [Test]
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
