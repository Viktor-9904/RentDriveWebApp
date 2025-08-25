using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.Company;

namespace RentDrive.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.CreatedOn)
                .IsRequired()
                .HasComment("User profile creation time (UTC).");

            builder
                .Property(u => u.UserType)
                .HasDefaultValue(UserType.Regular)
                .IsRequired()
                .HasComment("User's type.");
        }
    }
    public static class UserSeeder
    {
        public static async Task<IEnumerable<ApplicationUser>> SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new()
                {
                    Id = Guid.Parse(CompanyId),
                    UserName = "RENT-DRIVE",
                    Email = "RentDrive@RentDrive.com",
                    EmailConfirmed = true,
                    UserType = UserType.Company,
                    CreatedOn = new DateTime(2023, 7, 8),
                    Wallet = new Wallet
                    {
                        Id = Guid.Parse("d43e10f1-599f-4ab5-a246-2e2af3e0cab5"),
                        UserId = Guid.Parse(CompanyId),
                        Balance = 15220.32m,
                    },
                },
                new()
                {
                    Id = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                    UserName = "john.doe",
                    Email = "john.doe@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.Regular,
                    CreatedOn = new DateTime(2024, 8, 15),
                    Wallet = new Wallet
                    {
                        Id = Guid.Parse("fa010230-0d91-466b-a984-d47cd7651002"),
                        UserId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                        Balance = 4120.19m,
                    },
                },
                new()
                {
                    Id = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                    UserName = "jane.smith",
                    Email = "jane.smith@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.CompanyEmployee,
                    CreatedOn = new DateTime(2024, 9, 3),
                    Wallet = new Wallet
                    {
                        Id = Guid.Parse("d6b9cab2-c4f8-416c-8dbc-63bab5c5e860"),
                        UserId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                        Balance = 420.32m,
                    },
                },
                new()
                {
                    Id = Guid.Parse("a8b2e9f4-927d-4f87-a457-bf95cd4526dc"),
                    UserName = "alex.miles",
                    Email = "alex.miles@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.Regular,
                    CreatedOn = new DateTime(2025, 1, 12),
                    Wallet = new Wallet
                    {
                        Id = Guid.Parse("3286205e-24a5-47f7-a418-141ce2f61a7f"),
                        UserId = Guid.Parse("a8b2e9f4-927d-4f87-a457-bf95cd4526dc"),
                        Balance = 620.97m,
                    },
                }
            };

            List<ApplicationUser> seededUsers = new List<ApplicationUser>();

            foreach (ApplicationUser user in users)
            {
                ApplicationUser existingUser = await userManager.FindByIdAsync(user.Id.ToString());
                if (existingUser == null)
                {
                    IdentityResult result = await userManager.CreateAsync(user, "Asd123");

                    if (result.Succeeded)
                    {
                        seededUsers.Add(user);
                    }
                    else
                    {
                        throw new Exception($"Failed to seed user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            return seededUsers;
        }
    }
}
