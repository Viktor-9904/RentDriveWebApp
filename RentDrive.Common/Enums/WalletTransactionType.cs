using System.ComponentModel;

namespace RentDrive.Common.Enums
{
    public enum WalletTransactionType
    {
        [Description("Deposit")]
        Deposit = 1,

        [Description("Withdraw")]
        Withdraw = 2,

        [Description("Rental Payment")]
        RentalPayment = 3,

        [Description("Rental Profit")]
        RentalProfit = 4,

        [Description("Company Rental Fee Profit")]
        CompanyRentalFeeProfit = 5,

        [Description("Refund")]
        Refund = 6
    }
}
