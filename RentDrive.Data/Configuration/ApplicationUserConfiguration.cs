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
                    CreatedOn = new DateTime(2023, 7, 8)
                },
                new()
                {
                    Id = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                    UserName = "john.doe",
                    Email = "john.doe@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.Regular,
                    CreatedOn = new DateTime(2024, 8, 15)
                },
                new()
                {
                    Id = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                    UserName = "jane.smith",
                    Email = "jane.smith@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.CompanyEmployee,
                    CreatedOn = new DateTime(2024, 9, 3)
                },
                new()
                {
                    Id = Guid.Parse("a8b2e9f4-927d-4f87-a457-bf95cd4526dc"),
                    UserName = "alex.miles",
                    Email = "alex.miles@example.com",
                    EmailConfirmed = true,
                    UserType = UserType.Regular,
                    CreatedOn = new DateTime(2025, 1, 12)
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
