using Microsoft.VisualStudio.TestTools.UnitTesting;
using recruit_dotnetframework.Services;

namespace recruit_dotnetframework.Tests
{
    [TestClass]
    public class CreditCardServiceTests
    {
        private CreditCardService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new CreditCardService();
        }

        [TestMethod]
        public void IsValidCardNumber_ValidNumber_ReturnsTrue()
        {
            string validCardNumber = "1234567890123456";
            bool result = _service.IsValidCardNumber(validCardNumber);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidCardNumber_NullOrEmpty_ReturnsFalse()
        {
            Assert.IsFalse(_service.IsValidCardNumber(null));
            Assert.IsFalse(_service.IsValidCardNumber(string.Empty));
            Assert.IsFalse(_service.IsValidCardNumber("   "));
        }

        [TestMethod]
        public void IsValidCardNumber_IncorrectLength_ReturnsFalse()
        {
            Assert.IsFalse(_service.IsValidCardNumber("12345")); // Too short
            Assert.IsFalse(_service.IsValidCardNumber("12345678901234567")); // Too long
        }

        [TestMethod]
        public void IsValidCardNumber_ContainsNonDigits_ReturnsFalse()
        {
            Assert.IsFalse(_service.IsValidCardNumber("1234abcd5678efgh"));
        }
    }
}
