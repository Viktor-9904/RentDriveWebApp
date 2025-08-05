using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.ErrorValidationMessages.WalletTransactions.AddFundsValidationMessages;

namespace RentDrive.Web.ViewModels.WalletTransaction
{
    public class AddFundsViewModel
    {
        [Required(ErrorMessage = AmountIsRequired)]
        [Range(1, 1000000, ErrorMessage = AmountRange)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = CardHolderNameIsRequired)]
        public string CardHolderName { get; set; } = null!;

        [Required(ErrorMessage = CardNumberIsRequired)]
        [CreditCard(ErrorMessage = CardNumberInvalid)]
        public string CardNumber { get; set; } = null!;

        [Required(ErrorMessage = CVVIsRequired)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = CVVLength)]
        [RegularExpression(@"^\d{3}$", ErrorMessage = CVVInvalid)]
        public string Cvv { get; set; } = null!;

        [Required(ErrorMessage = ExpiryMonthIsRequired)]
        [StringLength(2, MinimumLength = 2, ErrorMessage = ExpiryMonthInvalid)]
        [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = ExpiryMonthInvalid)]
        public string ExpiryMonth { get; set; } = null!;

        [Required(ErrorMessage = ExpiryYearIsRequired)]
        public int ExpiryYear { get; set; }
    }
}
