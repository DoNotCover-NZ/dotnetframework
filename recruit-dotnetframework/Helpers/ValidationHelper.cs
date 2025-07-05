using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace recruit_dotnetframework.Helpers
{
    public static class ValidationHelper
    {
        private const int CardNumberLength = 16;
        private const string ExpiryDatePattern = @"^(0[1-9]|1[0-2])\/\d{2}$";
        private const string CvcPattern = @"^\d{3,4}$";
        private const int CenturyBase = 2000;

        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;
            if (cardNumber.Length != CardNumberLength) return false;
            return cardNumber.All(char.IsDigit);
        }

        public static bool IsValidCvc(string cvc)
        {
            if (string.IsNullOrWhiteSpace(cvc)) return false;
            return Regex.IsMatch(cvc, CvcPattern);
        }

        public static bool IsValidExpiry(string expiry)
        {
            if (string.IsNullOrWhiteSpace(expiry)) return false;
            if (!Regex.IsMatch(expiry, ExpiryDatePattern)) return false;

            var parts = expiry.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]) + CenturyBase;

            var expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return expiryDate >= DateTime.Now.Date;
        }
    }
}
