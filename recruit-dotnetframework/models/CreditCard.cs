using System.ComponentModel.DataAnnotations;

namespace recruit_dotnetframework.Models
{
    /// <summary>
    /// Represents a credit card entity.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Credit card number.
        /// </summary>
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// CVC code.
        /// </summary>
        [Required]
        public string Cvc { get; set; }

        /// <summary>
        /// Expiry date (MM/YY).
        /// </summary>
        [Required]
        public string Expiry { get; set; }
    }
}
