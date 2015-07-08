using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mapbox.vector.tile.tests
{
    [TestClass]
    public class ZigZagTests
    {
        [TestMethod]
        public void TestZigZagDecode()
        {
            // arrange
            const int inputVar = 1;

            // act
            var res = ZigZag.Decode(inputVar);

            // assert
            Assert.IsTrue(res == -1);
        }

        [TestMethod]
        public void AnotherTestZigZagDecode()
        {
            // arrange
            const int inputVar = 3;

            // act
            var res = ZigZag.Decode(inputVar);

            // assert
            Assert.IsTrue(res == -2 );
        }

        [TestMethod]
        public void TestZigZagEncode()
        {
            // arrange
            const int inputVar = -2;

            // act
            var res = ZigZag.Encode(inputVar);

            // assert
            Assert.IsTrue(res == 3);
        }

    }
}
