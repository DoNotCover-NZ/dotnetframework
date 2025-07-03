using System;
using System.Collections.Concurrent;
using recruit_dotnetframework.Models;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// In-memory repository for demo purposes.
    /// </summary>
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ConcurrentDictionary<string, CreditCard> _storage = new ConcurrentDictionary<string, CreditCard>();

        /// <summary>
        /// Adds or updates a credit card in the repository.
        /// </summary>
        /// <param name="card">Credit card to add or update.</param>
        public void Add(CreditCard card)
        {
            if (card == null || string.IsNullOrWhiteSpace(card.CardNumber))
                throw new ArgumentException("Invalid card");

            _storage[card.CardNumber] = card;
        }

        /// <summary>
        /// Gets a credit card by its card number.
        /// </summary>
        /// <param name="cardNumber">The card number to look up.</param>
        /// <returns>The credit card if found; otherwise, null.</returns>
        public CreditCard Get(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return null;

            _storage.TryGetValue(cardNumber, out var card);
            return card;
        }
    }
}
