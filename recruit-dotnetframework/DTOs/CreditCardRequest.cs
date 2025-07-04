using System.ComponentModel.DataAnnotations;

namespace recruit_dotnetframework.DTOs
{
    /// <summary>
    /// Request DTO for creating or updating a credit card.
    /// </summary>
    public class CreditCardRequest
    {
        /// <summary>
        /// Credit card number, exactly 16 digits.
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits.")]
        public string CardNumber { get; set; }

        /// <summary>
        /// CVC code, 3 or 4 digits.
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVC must be 3 or 4 digits.")]
        public string Cvc { get; set; }

        /// <summary>
        /// Expiry date in MM/YY format.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiry must be in MM/YY format.")]
        public string Expiry { get; set; }
    }
}
