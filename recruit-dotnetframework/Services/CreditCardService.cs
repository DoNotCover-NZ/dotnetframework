using recruit_dotnetframework.DTOs;
using recruit_dotnetframework.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// Service for managing credit card operations including validation and CRUD.
    /// </summary>
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _repository;

        /// <summary>
        /// Constructor injecting the credit card repository.
        /// </summary>
        /// <param name="repository">Repository to handle data persistence.</param>
        public CreditCardService(ICreditCardRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Validates if the card number is exactly 16 digits.
        /// </summary>
        /// <param name="cardNumber">Card number string.</param>
        /// <returns>True if valid; otherwise false.</returns>
        public bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;
            if (cardNumber.Length != 16) return false;
            return cardNumber.All(char.IsDigit);
        }

        /// <summary>
        /// Validates the CVC code (3 or 4 digits).
        /// </summary>
        /// <param name="cvc">CVC string.</param>
        /// <returns>True if valid; otherwise false.</returns>
        private bool IsValidCvc(string cvc)
        {
            if (string.IsNullOrWhiteSpace(cvc)) return false;
            return Regex.IsMatch(cvc, @"^\d{3,4}$");
        }

        /// <summary>
        /// Validates the expiry date in MM/YY format and checks if it is not expired.
        /// </summary>
        /// <param name="expiry">Expiry date string.</param>
        /// <returns>True if valid and not expired; otherwise false.</returns>
        private bool IsValidExpiry(string expiry)
        {
            if (string.IsNullOrWhiteSpace(expiry)) return false;
            if (!Regex.IsMatch(expiry, @"^(0[1-9]|1[0-2])\/\d{2}$")) return false;

            var parts = expiry.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]) + 2000;

            var expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return expiryDate >= DateTime.Now.Date;
        }

        /// <summary>
        /// Creates a new credit card record.
        /// </summary>
        /// <param name="request">Credit card request data.</param>
        /// <returns>Created credit card response.</returns>
        /// <exception cref="ArgumentException">Throws when validation fails.</exception>
        public CreditCardResponse CreateCreditCard(CreditCardRequest request)
        {
            if (!IsValidCardNumber(request.CardNumber))
                throw new ArgumentException("Invalid card number.");

            if (!IsValidCvc(request.Cvc))
                throw new ArgumentException("Invalid CVC.");

            if (!IsValidExpiry(request.Expiry))
                throw new ArgumentException("Invalid or expired expiry date.");

            var card = new CreditCard
            {
                CardNumber = request.CardNumber,
                Cvc = request.Cvc,
                Expiry = request.Expiry
            };

            _repository.Add(card);

            return new CreditCardResponse
            {
                CardNumber = card.CardNumber,
                Cvc = card.Cvc,
                Expiry = card.Expiry
            };
        }

        /// <summary>
        /// Retrieves a credit card by card number.
        /// </summary>
        /// <param name="cardNumber">Card number to retrieve.</param>
        /// <returns>Credit card response or null if not found.</returns>
        public CreditCardResponse GetCreditCard(string cardNumber)
        {
            var card = _repository.Get(cardNumber);
            if (card == null) return null;

            return new CreditCardResponse
            {
                CardNumber = card.CardNumber,
                Cvc = card.Cvc,
                Expiry = card.Expiry
            };
        }

        /// <summary>
        /// Updates an existing credit card record.
        /// </summary>
        /// <param name="cardNumber">Card number of the card to update.</param>
        /// <param name="request">Updated credit card data.</param>
        /// <returns>Updated credit card response.</returns>
        /// <exception cref="ArgumentException">Throws when validation fails or card not found.</exception>
        public CreditCardResponse UpdateCreditCard(string cardNumber, CreditCardRequest request)
        {
            if (cardNumber != request.CardNumber)
                throw new ArgumentException("Card number in path and request body do not match.");

            var existingCard = _repository.Get(cardNumber);
            if (existingCard == null)
                throw new ArgumentException("Card not found.");

            if (!IsValidCardNumber(request.CardNumber))
                throw new ArgumentException("Invalid card number.");

            if (!IsValidCvc(request.Cvc))
                throw new ArgumentException("Invalid CVC.");

            if (!IsValidExpiry(request.Expiry))
                throw new ArgumentException("Invalid or expired expiry date.");

            existingCard.Cvc = request.Cvc;
            existingCard.Expiry = request.Expiry;

            _repository.Update(existingCard);

            return new CreditCardResponse
            {
                CardNumber = existingCard.CardNumber,
                Cvc = existingCard.Cvc,
                Expiry = existingCard.Expiry
            };
        }

        /// <summary>
        /// Deletes a credit card by card number.
        /// </summary>
        /// <param name="cardNumber">Card number to delete.</param>
        /// <exception cref="ArgumentException">Throws when card not found.</exception>
        public void DeleteCreditCard(string cardNumber)
        {
            var existingCard = _repository.Get(cardNumber);
            if (existingCard == null)
                throw new ArgumentException("Card not found.");

            _repository.Delete(cardNumber);
        }
    }
}
