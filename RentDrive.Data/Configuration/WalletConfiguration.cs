using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    public static class WalletSeeder
    {
        public static IEnumerable<Wallet> SeedUserWallets()
        {
            IEnumerable<Wallet> userWallets = new List<Wallet>
            {
                new()
                {
                    Id = Guid.Parse("d6b9cab2-c4f8-416c-8dbc-63bab5c5e860"),
                    UserId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                    Balance = 420.32m,
                },
                new()
                {
                    Id = Guid.Parse("3286205e-24a5-47f7-a418-141ce2f61a7f"),
                    UserId = Guid.Parse("a8b2e9f4-927d-4f87-a457-bf95cd4526dc"),
                    Balance = 620.97m,
                },
                new()
                {
                    Id = Guid.Parse("fa010230-0d91-466b-a984-d47cd7651002"),
                    UserId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                    Balance = 4120.19m,
                },
            };

            return userWallets;
        }
    }
}
