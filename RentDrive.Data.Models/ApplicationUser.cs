using Microsoft.AspNetCore.Identity;

namespace RentDrive.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }
        public DateTime CreatedOn { get; set; }
        public bool IsCompanyEmployee { get; set; }
    }
}
