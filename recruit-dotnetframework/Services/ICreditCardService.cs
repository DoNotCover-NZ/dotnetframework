using recruit_dotnetframework.DTOs;

namespace recruit_dotnetframework.Services
{
    /// <summary>
    /// Service interface for managing credit cards.
    /// </summary>
    public interface ICreditCardService
    {
        /// <summary>
        /// Creates a new credit card record.
        /// </summary>
        /// <param name="request">Credit card data.</param>
        /// <returns>The created credit card information.</returns>
        CreditCardResponse CreateCreditCard(CreditCardRequest request);

        /// <summary>
        /// Retrieves a credit card by its card number.
        /// </summary>
        /// <param name="cardNumber">Credit card number.</param>
        /// <returns>The credit card data if found; otherwise, null.</returns>
        CreditCardResponse GetCreditCard(string cardNumber);

        /// <summary>
        /// Updates an existing credit card record.
        /// </summary>
        /// <param name="cardNumber">The card number to update.</param>
        /// <param name="request">Updated credit card data.</param>
        /// <returns>The updated credit card information.</returns>
        CreditCardResponse UpdateCreditCard(string cardNumber, CreditCardRequest request);

        /// <summary>
        /// Deletes a credit card record by card number.
        /// </summary>
        /// <param name="cardNumber">The card number to delete.</param>
        /// <returns>True if deletion succeeded; otherwise false.</returns>
        void DeleteCreditCard(string cardNumber);
    }
}
