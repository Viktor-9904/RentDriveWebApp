using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Data.Models;

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
                .Property(u => u.IsCompanyEmployee)
                .HasDefaultValue(false)
                .IsRequired()
                .HasComment("Is the user employee of the company.");
        }
    }
}
