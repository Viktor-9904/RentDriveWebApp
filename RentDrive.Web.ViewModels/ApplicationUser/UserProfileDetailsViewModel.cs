using System.ComponentModel.DataAnnotations;
using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.ApplicationUser;

namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public class UserProfileDetailsViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(usernameMinLength, ErrorMessage = "Username must be at least 3 characters long")]
        [MaxLength(usernameMaxLength, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\+?[0-9]{8,15}$", ErrorMessage = "Phone number must be 8–15 digits, optionally starting with +")]
        public string? PhoneNumber { get; set; }
    }
}
