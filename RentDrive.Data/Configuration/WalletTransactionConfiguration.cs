using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Data.Models;

namespace RentDrive.Data.Configuration
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder
                .HasKey(wt => wt.Id);

            builder
                .HasOne(wt => wt.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(wt => wt.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(wt => wt.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasComment("Amount of money");

            builder
                .Property(wt => wt.Type)
                .IsRequired()
                .HasComment("Type of transaction - Deposit or Withdraw.");

            builder
                .Property(wt => wt.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()")
                .HasComment("Creation date of transaction.");
        }
    }
}
