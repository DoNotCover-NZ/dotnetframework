using recruit_dotnetframework.Models;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// Interface for credit card repository.
    /// </summary>
    public interface ICreditCardRepository
    {
        /// <summary>
        /// Adds or updates a credit card in the repository.
        /// </summary>
        /// <param name="card">Credit card to add or update.</param>
        void Add(CreditCard card);

        /// <summary>
        /// Retrieves a credit card by its card number.
        /// </summary>
        /// <param name="cardNumber">The credit card number.</param>
        /// <returns>The credit card if found; otherwise, null.</returns>
        CreditCard Get(string cardNumber);
    }
}
