using recruit_dotnetframework.Models;
using System;
using System.Collections.Concurrent;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// In-memory repository for managing credit card records.
    /// Provides methods to add, retrieve, update, and delete credit card information.
    /// </summary>
    public class CreditCardRepository : ICreditCardRepository
    {
        // In-memory storage of credit cards using a concurrent dictionary.
        private readonly ConcurrentDictionary<string, CreditCard> _storage = new ConcurrentDictionary<string, CreditCard>();

        /// <summary>
        /// Adds a new credit card to the repository or updates the existing one.
        /// </summary>
        /// <param name="card">The credit card to add or update.</param>
        /// <exception cref="ArgumentException">Throws when the card is invalid.</exception>
        public void Add(CreditCard card)
        {
            if (card == null || string.IsNullOrWhiteSpace(card.CardNumber))
                throw new ArgumentException("Invalid card");

            // Adds or updates the card in the dictionary (based on card number).
            _storage[card.CardNumber] = card;
        }

        /// <summary>
        /// Retrieves a credit card from the repository by its card number.
        /// </summary>
        /// <param name="cardNumber">The card number to look up.</param>
        /// <returns>The credit card if found; otherwise, null.</returns>
        public CreditCard Get(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return null;
            
            // Tries to get the card from the storage.
            _storage.TryGetValue(cardNumber, out var card);
            return card;
        }

        /// <summary>
        /// Updates an existing credit card in the repository.
        /// </summary>
        /// <param name="card">The credit card to update.</param>
        /// <exception cref="ArgumentException">Throws when the card is invalid or not found.</exception>
        public void Update(CreditCard card)
        {
            if (card == null || string.IsNullOrWhiteSpace(card.CardNumber))
                throw new ArgumentException("Invalid card");

            // Check if the card exists and update it, or else throw an exception.
            if (!_storage.ContainsKey(card.CardNumber))
                throw new ArgumentException("Card not found.");

            _storage[card.CardNumber] = card; // Updates the existing card
        }

        /// <summary>
        /// Deletes a credit card from the repository by its card number.
        /// </summary>
        /// <param name="cardNumber">The card number to delete.</param>
        /// <exception cref="ArgumentException">Throws when the card is not found.</exception>
        public void Delete(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty");

            // Try to remove the card from the storage. If the card doesn't exist, throw an exception.
            if (!_storage.TryRemove(cardNumber, out _))
            {
                throw new ArgumentException("Card not found.");
            }
        }
    }
}
