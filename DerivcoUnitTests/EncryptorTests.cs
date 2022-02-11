using Microsoft.VisualStudio.TestTools.UnitTesting;

using DerivcoTest;

namespace Question1Tests
{
    [TestClass]
    public class EncryptorTests
    {
        [TestMethod]
        public void ShouldEncodeString()
        {
            //prepare
            //var input = "1";
            //var expected = "MQ==";
            var input = "Test string Test string";
            var expected = "VGVzdCBzdHJpbmcgVGVzdCBzdHJpbmc=";
            
            //act 
            var actual = Encryptor.Encode(input);

            //check
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldDecodeString()
        {
            //prepare
            var expected = "Test string Test string";
            var input = "VGVzdCBzdHJpbmcgVGVzdCBzdHJpbmc=";

            //act
            var actual = Encryptor.Decode(input);

            //check
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldEncodeEmptyString()
        {
            //prepare
            var input = "";
            var expected = "";

            //act
            var actual = Encryptor.Encode(input);

            //check
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldEncodeAndDecodeStringWithSpecialChars()
        {
            //prepare
            var input = "Test -$%^& string";
            
            //act
            var actualEncoded = Encryptor.Encode(input);
            var actualDecoded = Encryptor.Decode(actualEncoded);

            //check
            Assert.AreEqual(input, actualDecoded);
        }
    }
}
