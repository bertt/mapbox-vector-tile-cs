using System.Reflection;
using GeoJSON.Net;
using Mapbox.Vectors.mapnik.vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;

namespace Mapbox.Vectors.Tests
{
    [TestClass]
    public class TileParserTests
    {
        [TestMethod]
        public void TestBagVecorTile()
        {
            // arrange
            const string bagfile = "Mapbox.Vectors.testdata.bag.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bagfile);
            var tile = Serializer.Deserialize<tile>(pbfStream);
            var layerInfos = TileParser.Parse(tile);

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
            var tile = Serializer.Deserialize<tile>(pbfStream);
            var layerInfos = TileParser.Parse(tile);

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
            var tile = Serializer.Deserialize<tile>(pbfStream);
            var layerInfos = TileParser.Parse(tile);

            // assert
            Assert.IsTrue(layerInfos.Count == 11);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count==256);
            Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count == 1);

        }


    }
}
