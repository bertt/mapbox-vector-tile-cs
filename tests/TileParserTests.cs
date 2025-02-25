using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Mapbox.Vector.Tile.Tile;

namespace Mapbox.Vector.Tile.tests;

public class TileParserTests
{
    [Test]
    // test for issue 10 https://github.com/bertt/mapbox-vector-tile-cs/issues/10
    // Attributes tests for short Int values
    public void TestIssue10MapBoxVectorTile()
    {
        // arrange
        const string mapboxissue10File = "cadastral.pbf";

        // act
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapboxissue10File));
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // asserts
        var firstattribute = layerInfos[0].VectorTileFeatures[0].Attributes[0];
        var val = firstattribute.Value;
        Assert.That((long)val == 867160);
    }

    [Test]
    // test for issue 3 https://github.com/bertt/mapbox-vector-tile-cs/issues/3
    // tile: https://b.tiles.mapbox.com/v4/mapbox.mapbox-terrain-v2,mapbox.mapbox-streets-v7/13/4260/2911.vector.pbf
    public void TestIssue3MapBoxVectorTile()
    {
        // arrange
        const string mapboxissue3File = "issue3_2911.vector.pbf";

        // act
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapboxissue3File));
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // asserts
        Assert.That(layerInfos[7].VectorTileFeatures.Count == 225);
        Assert.That(layerInfos[0].Version == 2);
        Assert.That(layerInfos[7].Name == "road");
        Assert.That(layerInfos[7].Extent == 4096);
        var firstroad = layerInfos[7].VectorTileFeatures[0];
        Assert.That(firstroad.Geometry.Count == 5);
        Assert.That(firstroad.Geometry[0].Count == 1);
        Assert.That(firstroad.Geometry[0][0].X == 816);
        Assert.That(firstroad.Geometry[0][0].Y == 3446);

        var secondroad = layerInfos[7].VectorTileFeatures[1];
        Assert.That(secondroad.Geometry.Count == 2);
        Assert.That(secondroad.Geometry[0].Count == 9);
        Assert.That(secondroad.Geometry[0][0].X == 3281);
        Assert.That(secondroad.Geometry[0][0].Y == 424);
    }

    [Test]
    public void TestBagVectorTile()
    {
        // arrange
        const string bagfile = "bag-17-67317-43082.pbf";

        // act
        var pbfStream = File.OpenRead(Path.Combine("testdata", bagfile));
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // assert
        Assert.That(layerInfos.Count == 1);
        Assert.That(layerInfos[0].VectorTileFeatures.Count == 83);
        Assert.That(layerInfos[0].VectorTileFeatures[0].GeometryType == GeomType.Polygon);
    }

    [Test]
    public async Task TestTrailViewTileFromUrl()
    {
        // arrange
        var url = "https://trailview-tiles.maps.komoot.net/tiles/v2/9/264/167.vector.pbf";

        // Note: Use HttpClient with automatic decompression 
        // instead of regular HttpClient otherwise we get exception 
        // 'ProtoBuf.ProtoException: Invalid wire-type; this usually means you have over-written a file without truncating or setting the length'
        var gzipWebClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
        var bytes = await gzipWebClient.GetByteArrayAsync(url);

        var stream = new MemoryStream(bytes);

        // act
        var layerInfos = VectorTileParser.Parse(stream);

        // assert
        Assert.That(layerInfos.Count > 0);
    }


    [Test]
    public void TestMapzenTile()
    {
        // arrange
        const string mapzenfile = "mapzen000.mvt";

        // act
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapzenfile));
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // assert
        Assert.That(layerInfos.Count == 10);
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
        const string mapboxfile = "14-8801-5371.vector.pbf";

        // act
        var pbfStream = File.OpenRead(Path.Combine("testdata", mapboxfile));
        var layerInfos = VectorTileParser.Parse(pbfStream);

        // check features
        Assert.That(layerInfos.Count == 20);
        Assert.That(layerInfos[0].VectorTileFeatures.Count == 107);
        Assert.That(layerInfos[0].VectorTileFeatures[0].Attributes.Count == 2);

        // check park feature
        var park = layerInfos[17].VectorTileFeatures[11];
        var firstOrDefault = (from prop in park.Attributes where prop.Key == "name" select prop.Value).FirstOrDefault();
        if (firstOrDefault != null)
        {
            var namePark = firstOrDefault.ToString();
            Assert.That(namePark == "Mauerpark");
        }

        // check point geometry type from park
        Assert.That(park.Id == "3000003150561");
        Assert.That(park.GeometryType == GeomType.Point);
        Assert.That(park.Geometry.Count == 1);
        Assert.That(park.Geometry[0].Count == 1);
        var p = park.Geometry[0][0];
        Assert.That(Math.Abs(p.X - 3898) < 0.1);
        Assert.That(Math.Abs(p.Y - 1731) < 0.1);

        // Check line geometry from roads
        var road = layerInfos[8].VectorTileFeatures[656];
        Assert.That(road.Id == "241452814");
        Assert.That(road.GeometryType == GeomType.LineString);
        var ls = road.Geometry;
        Assert.That(ls.Count == 1);
        Assert.That(ls[0].Count == 3);
        var firstPoint = ls[0][0];
        Assert.That(Math.Abs(firstPoint.X - 1988) < 0.1);
        Assert.That(Math.Abs(firstPoint.Y - 306) < 0.1);

        var secondPoint = ls[0][1];
        Assert.That(Math.Abs(secondPoint.X - 1808) < 0.1);
        Assert.That(Math.Abs(secondPoint.Y - 321) < 0.1);

        var thirdPoint = ls[0][2];
        Assert.That(Math.Abs(thirdPoint.X - 1506) < 0.1);
        Assert.That(Math.Abs(thirdPoint.Y - 347) < 0.1);

        // Check polygon geometry for buildings
        var building = layerInfos[5].VectorTileFeatures[0];
        Assert.That(building.Id == "1000267229912");
        Assert.That(building.GeometryType == GeomType.Polygon);
        var b = building.Geometry;
        Assert.That(b.Count == 1);
        Assert.That(b[0].Count == 5);
        firstPoint = b[0][0];
        Assert.That(Math.Abs(firstPoint.X - 2039) < 0.1);
        Assert.That(Math.Abs(firstPoint.Y + 32) < 0.1);
        secondPoint = b[0][1];
        Assert.That(Math.Abs(secondPoint.X - 2035) < 0.1);
        Assert.That(Math.Abs(secondPoint.Y + 31) < 0.1);
        thirdPoint = b[0][2];
        Assert.That(Math.Abs(thirdPoint.X - 2032) < 0.1);
        Assert.That(Math.Abs(thirdPoint.Y + 31) < 0.1);
        var fourthPoint = b[0][3];
        Assert.That(Math.Abs(fourthPoint.X - 2032) < 0.1);
        Assert.That(Math.Abs(fourthPoint.Y + 32) < 0.1);
        var fifthPoint = b[0][4];
        Assert.That(Math.Abs(fifthPoint.X - 2039) < 0.1);
        Assert.That(Math.Abs(fifthPoint.Y + 32) < 0.1);
    }

}
