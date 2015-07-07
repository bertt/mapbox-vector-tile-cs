using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mapbox.Vectors.Sample.tests
{
    [TestClass]
    public class ZigZagDecoderTests
    {
        [TestMethod]
        public void TestZigZagDecode()
        {
            // arrange
            const int inputVar = 10;

            // act
            var res = ZigZagDecoder.ZigZagDecode(inputVar);

            // assert
            Assert.IsTrue(res == 5);
        }

        [TestMethod]
        public void AnotherTestZigZagDecode()
        {
            // arrange
            const int inputVar = 255;

            // act
            var res = ZigZagDecoder.ZigZagDecode(inputVar);

            // assert
            Assert.IsTrue(res == -128);
        }

    }
}
