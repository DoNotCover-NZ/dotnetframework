using System.Linq;

namespace recruit_dotnetframework.Services
{
    public class CreditCardService
    {
        /// <summary>
        /// Validates if the credit card number is exactly 16 digits long and numeric.
        /// </summary>
        /// <param name="cardNumber">The credit card number string.</param>
        /// <returns>True if valid; otherwise false.</returns>
        public bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            if (cardNumber.Length != 16)
                return false;

            return cardNumber.All(char.IsDigit);
        }
    }
}
