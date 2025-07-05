using recruit_dotnetframework.DTOs;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.Helpers;
using System;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// Service for managing credit card operations including CRUD.
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
        /// Creates a new credit card record.
        /// </summary>
        /// <param name="request">Credit card request data.</param>
        /// <returns>Created credit card response.</returns>
        /// <exception cref="ArgumentException">Throws when validation fails.</exception>
        public CreditCardResponse CreateCreditCard(CreditCardRequest request)
        {
            if (!ValidationHelper.IsValidCardNumber(request.CardNumber))
                throw new ArgumentException("Invalid card number.");

            if (!ValidationHelper.IsValidCvc(request.Cvc))
                throw new ArgumentException("Invalid CVC.");

            if (!ValidationHelper.IsValidExpiry(request.Expiry))
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

            if (!ValidationHelper.IsValidCardNumber(request.CardNumber))
                throw new ArgumentException("Invalid card number.");

            if (!ValidationHelper.IsValidCvc(request.Cvc))
                throw new ArgumentException("Invalid CVC.");

            if (!ValidationHelper.IsValidExpiry(request.Expiry))
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
