using System.Collections.Generic;
using System.Linq;
using Mapbox.Vectors.mapnik.vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mapbox.Vectors.tests
{
    [TestClass]
    public class GeometryParserTests
    {
        [TestMethod]
        public void GeometryParserTest()
        {
            // arrange
            // input: [9,0,8192,26,0,10,2,0,0,2,15]
            var input = new List<uint>{9, 0, 8192, 26, 0, 10, 2, 0, 0, 2, 15};

            // act
            var output = GeometryParser.ParseGeometry(input, tile.GeomType.Point);
            // assert
            // expected result according to python code: [[0, 0], [0, -5], [1, -5], [1, -6]]
            Assert.IsTrue(output.ToList().Count == 4);
            Assert.IsTrue(output[0].Longitude==0);
            Assert.IsTrue(output[0].Latitude== 0);
            Assert.IsTrue(output[1].Longitude == 0);
            Assert.IsTrue(output[1].Latitude == -5);
            Assert.IsTrue(output[2].Longitude == 1);
            Assert.IsTrue(output[2].Latitude == -5);
            Assert.IsTrue(output[3].Longitude == 1);
            Assert.IsTrue(output[3].Latitude == -6);
        }


        [TestMethod]
        public void AnotherGeometryParserTest()
        {
            // arrange
            var input = new List<uint> {9, 0, 1540, 34, 48, 12, 31, 224, 15, 7, 0, 227, 15};
            // act
            var output = GeometryParser.ParseGeometry(input, tile.GeomType.Polygon);

            // assert
            // expected result according to python code: [[0, 3326], [24, 3320], [8, 3208], [0, 3212], [0, 3326],[0, 3326] ]
            Assert.IsTrue(output.ToList().Count == 6);
            Assert.IsTrue(output[0].Longitude == 0);
            Assert.IsTrue(output[0].Latitude == 3326);
            Assert.IsTrue(output[1].Longitude == 24);
            Assert.IsTrue(output[1].Latitude == 3320);
            Assert.IsTrue(output[2].Longitude == 8);
            Assert.IsTrue(output[2].Latitude == 3208);
            Assert.IsTrue(output[3].Longitude == 0);
            Assert.IsTrue(output[3].Latitude == 3212);
            Assert.IsTrue(output[4].Longitude == 0);
            Assert.IsTrue(output[4].Latitude == 3326);
            Assert.IsTrue(output[5].Longitude == 0);
            Assert.IsTrue(output[5].Latitude == 3326);
        }


        [TestMethod]
        public void AndAnotherGeometryParserTest()
        {
            // arrange
            var input = new List<uint> { 9, 255, 255, 10, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15, 9, 0, 0, 15 };
            // act
            var output = GeometryParser.ParseGeometry(input, tile.GeomType.Polygon);

            // assert
            // expected result according to python code: [[-128, 4224], [-128, 4224], [-128, 4224]]
            Assert.IsTrue(output.ToList().Count == 3);
            Assert.IsTrue(output[0].Longitude == -128);
            Assert.IsTrue(output[0].Latitude == 4224);
            Assert.IsTrue(output[1].Longitude == -128);
            Assert.IsTrue(output[1].Latitude == 4224);
            Assert.IsTrue(output[2].Longitude == -128);
            Assert.IsTrue(output[2].Latitude == 4224);
        }

    }
}
