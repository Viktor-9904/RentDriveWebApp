namespace RentDrive.Common.ErrorValidationMessages
{
    public static class WalletTransactions
    {
        public static class AddFundsValidationMessages
        {
            public const string AmountIsRequired = "Amount is required!";
            public const string AmountRange = "Amount must be between 1 and 1,000,000.";

            public const string CardHolderNameIsRequired = "Cardholder name is required!";
            public const string CardHolderNameMaxLength = "Cardholder name is too long.";

            public const string CardNumberIsRequired = "Card number is required!";
            public const string CardNumberInvalid = "Invalid card number.";

            public const string CVVIsRequired = "CVV is required!";
            public const string CVVLength = "CVV must be exactly 3 digits.";
            public const string CVVInvalid = "CVV must be numeric.";

            public const string ExpiryMonthIsRequired = "Expiry month is required!";
            public const string ExpiryMonthInvalid = "Expiry month must be between 01 and 12.";

            public const string ExpiryYearIsRequired = "Expiry year is required!";
        }
    }
}
