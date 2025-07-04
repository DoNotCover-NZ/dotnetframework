namespace recruit_dotnetframework.DTOs
{
    /// <summary>
    /// Response DTO for credit card data.
    /// </summary>
    public class CreditCardResponse
    {
        /// <summary>
        /// Credit card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// CVC code.
        /// </summary>
        public string Cvc { get; set; }

        /// <summary>
        /// Expiry date in MM/YY format.
        /// </summary>
        public string Expiry { get; set; }
    }
}
