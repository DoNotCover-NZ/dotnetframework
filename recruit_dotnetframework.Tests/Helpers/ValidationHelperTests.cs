using Microsoft.VisualStudio.TestTools.UnitTesting;
using recruit_dotnetframework.Helpers;

namespace recruit_dotnetframework.Tests
{
    [TestClass]
    public class ValidationHelperTests
    {
        [TestMethod]
        public void IsValidCardNumber_ValidNumber_ReturnsTrue()
        {
            Assert.IsTrue(ValidationHelper.IsValidCardNumber("1234567890123456"));
        }

        [TestMethod]
        public void IsValidCardNumber_NullOrEmpty_ReturnsFalse()
        {
            Assert.IsFalse(ValidationHelper.IsValidCardNumber(null));
            Assert.IsFalse(ValidationHelper.IsValidCardNumber(string.Empty));
            Assert.IsFalse(ValidationHelper.IsValidCardNumber("   "));
        }

        [TestMethod]
        public void IsValidCardNumber_IncorrectLength_ReturnsFalse()
        {
            Assert.IsFalse(ValidationHelper.IsValidCardNumber("12345"));
            Assert.IsFalse(ValidationHelper.IsValidCardNumber("12345678901234567"));
        }

        [TestMethod]
        public void IsValidCardNumber_ContainsNonDigits_ReturnsFalse()
        {
            Assert.IsFalse(ValidationHelper.IsValidCardNumber("1234abcd5678efgh"));
        }

        [TestMethod]
        public void IsValidCvc_ValidCases_ReturnsTrue()
        {
            Assert.IsTrue(ValidationHelper.IsValidCvc("123"));
            Assert.IsTrue(ValidationHelper.IsValidCvc("9999"));
        }

        [TestMethod]
        public void IsValidCvc_InvalidCases_ReturnsFalse()
        {
            Assert.IsFalse(ValidationHelper.IsValidCvc(null));
            Assert.IsFalse(ValidationHelper.IsValidCvc(""));
            Assert.IsFalse(ValidationHelper.IsValidCvc("12"));
            Assert.IsFalse(ValidationHelper.IsValidCvc("abcd"));
            Assert.IsFalse(ValidationHelper.IsValidCvc("12345"));
        }

        [TestMethod]
        public void IsValidExpiry_ValidCase_ReturnsTrue()
        {
            Assert.IsTrue(ValidationHelper.IsValidExpiry("12/99"));
        }

        [TestMethod]
        public void IsValidExpiry_InvalidCases_ReturnsFalse()
        {
            Assert.IsFalse(ValidationHelper.IsValidExpiry(null));
            Assert.IsFalse(ValidationHelper.IsValidExpiry(""));
            Assert.IsFalse(ValidationHelper.IsValidExpiry("13/25"));
            Assert.IsFalse(ValidationHelper.IsValidExpiry("00/20"));
            Assert.IsFalse(ValidationHelper.IsValidExpiry("abc/yy"));
            Assert.IsFalse(ValidationHelper.IsValidExpiry("12/10"));
        }
    }
}
