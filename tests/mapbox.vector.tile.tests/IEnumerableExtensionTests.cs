using System.Collections.Generic;
using System.Linq;
using Mapbox.Vectors.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mapbox.Vectors.tests
{
    [TestClass]
    public class EnumerableExtensionTests
    {
        [TestMethod]
        public void TestEvensMethod()
        {
            // arrange
            var sequence = new List<int> { 0, 1, 2, 3 };

            // act
            var evens = sequence.GetEvens();

            // assert
            Assert.IsTrue(evens.ToList()[0]==0);
            Assert.IsTrue(evens.ToList()[1]==2);
        }

        [TestMethod]
        public void TestOddsMethod()
        {
            // arrange
            var sequence = new List<int> { 0, 1, 2, 3 };

            // act
            var evens = sequence.GetOdds();

            // assert
            Assert.IsTrue(evens.ToList()[0] == 1);
            Assert.IsTrue(evens.ToList()[1] == 3);
        }
    }
}
