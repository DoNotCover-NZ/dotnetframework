using recruit_dotnetframework.Models;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// Interface for credit card repository.
    /// Defines CRUD operations for managing credit card data.
    /// </summary>
    public interface ICreditCardRepository
    {
        /// <summary>
        /// Adds a new credit card to the repository. 
        /// If the card already exists, it will be replaced.
        /// </summary>
        /// <param name="card">Credit card to add.</param>
        void Add(CreditCard card);

        /// <summary>
        /// Retrieves a credit card by its card number.
        /// </summary>
        /// <param name="cardNumber">The credit card number to look up.</param>
        /// <returns>The credit card if found; otherwise, null.</returns>
        CreditCard Get(string cardNumber);

        /// <summary>
        /// Updates an existing credit card in the repository.
        /// </summary>
        /// <param name="card">Credit card with updated data.</param>
        /// <exception cref="ArgumentException">Throws when the card is not found.</exception>
        void Update(CreditCard card);

        /// <summary>
        /// Deletes a credit card from the repository by card number.
        /// </summary>
        /// <param name="cardNumber">The credit card number to delete.</param>
        /// <exception cref="ArgumentException">Throws when the card is not found.</exception>
        void Delete(string cardNumber);
    }
}
