using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using RentDrive.Data.Models;

namespace RentDrive.Data.Configuration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder
                .HasKey(w => w.Id);

            builder
                .HasIndex(w => w.UserId)
                .IsUnique();

            builder
                .HasOne(w => w.User)
                .WithOne(au => au.Wallet)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(w => w.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasComment("Available balance in wallet.");
        }
    }
}
