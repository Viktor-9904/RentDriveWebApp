using System.ComponentModel.DataAnnotations;
using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.ApplicationUser;

namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public class UserChangePasswordInpuViewModel
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "New Password is required.")]
        [MinLength(passwordMinLength, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(passwordMaxLength, ErrorMessage = "Password must not exceed 50 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
