using System.Collections.Generic;
using System.Linq;
using mapbox.vector.tile.ExtensionMethods;
using NUnit.Framework;

namespace mapbox.vector.tile.tests
{
    public class EnumerableExtensionTests
    {
        [Test]
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

        [Test]
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
