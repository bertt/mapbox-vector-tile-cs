using System.Collections.Generic;
using System.Linq;
using mapbox.vector.tile.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mapbox.vector.tile.tests
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
            var evens = sequence.GetEvens().ToList();

            // assert
            Assert.IsTrue(evens[0]==0);
            Assert.IsTrue(evens[1]==2);
        }

        [TestMethod]
        public void TestOddsMethod()
        {
            // arrange
            var sequence = new List<int> { 0, 1, 2, 3 };

            // act
            var evens = sequence.GetOdds().ToList();

            // assert
            Assert.IsTrue(evens[0] == 1);
            Assert.IsTrue(evens[1] == 3);
        }
    }
}
