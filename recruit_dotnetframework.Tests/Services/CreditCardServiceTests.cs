using Microsoft.VisualStudio.TestTools.UnitTesting;
using recruit_dotnetframework.Services;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.DTOs;
using System;

namespace recruit_dotnetframework.Tests
{
    [TestClass]
    public class CreditCardServiceTests
    {
        private CreditCardService _service;

        [TestInitialize]
        public void Setup()
        {
            var repository = new CreditCardRepository();
            _service = new CreditCardService(repository);
        }

        [TestMethod]
        public void CreateCreditCard_ValidData_Succeeds()
        {
            var request = new CreditCardRequest
            {
                CardNumber = "1234567890123456",
                Cvc = "123",
                Expiry = "12/30"
            };

            var response = _service.CreateCreditCard(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(request.CardNumber, response.CardNumber);
            Assert.AreEqual(request.Cvc, response.Cvc);
            Assert.AreEqual(request.Expiry, response.Expiry);
        }

        [TestMethod]
        public void GetCreditCard_ExistingCard_ReturnsCard()
        {
            var request = new CreditCardRequest
            {
                CardNumber = "1234567890123456",
                Cvc = "123",
                Expiry = "12/30"
            };

            _service.CreateCreditCard(request);

            var result = _service.GetCreditCard(request.CardNumber);

            Assert.IsNotNull(result);
            Assert.AreEqual(request.CardNumber, result.CardNumber);
        }

        [TestMethod]
        public void UpdateCreditCard_ValidData_Succeeds()
        {
            var original = new CreditCardRequest
            {
                CardNumber = "1234567890123456",
                Cvc = "123",
                Expiry = "12/30"
            };

            _service.CreateCreditCard(original);

            var updated = new CreditCardRequest
            {
                CardNumber = "1234567890123456",
                Cvc = "999",
                Expiry = "11/33"
            };

            var response = _service.UpdateCreditCard(original.CardNumber, updated);

            Assert.IsNotNull(response);
            Assert.AreEqual(updated.Cvc, response.Cvc);
            Assert.AreEqual(updated.Expiry, response.Expiry);
        }

        [TestMethod]
        public void DeleteCreditCard_ExistingCard_Succeeds()
        {
            var request = new CreditCardRequest
            {
                CardNumber = "1234567890123456",
                Cvc = "123",
                Expiry = "12/30"
            };

            _service.CreateCreditCard(request);

            _service.DeleteCreditCard(request.CardNumber);

            var deleted = _service.GetCreditCard(request.CardNumber);
            Assert.IsNull(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCreditCard_NonExistingCard_Throws()
        {
            _service.DeleteCreditCard("0000000000000000");
        }
    }
}
