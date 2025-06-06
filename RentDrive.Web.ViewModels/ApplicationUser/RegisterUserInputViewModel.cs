using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.ApplicationUser;
using static RentDrive.Common.ErrorValidationMessages.ApplicationUserErrorMessages.ApplicationUser;

namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public class RegisterUserInputViewModel
    {
        [Required(ErrorMessage = usernameIsRequired)]
        [MinLength(usernameMinLength, ErrorMessage = usernameTooShortErrorMessage)]
        [MaxLength(usernameMaxLength, ErrorMessage = usernameTooLongErrorMessage)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = passwordIsRequired)]
        [MinLength(passwordMinLength, ErrorMessage = passwordTooShortErrorMessage)]
        [MaxLength(passwordMaxLength, ErrorMessage = passwordTooLongErrorMessage)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = comfirmedPasswordIsRequired)]
        [Compare("Password", ErrorMessage = comfirmedPasswordDoesntMatch)]
        public string ComfirmedPassword { get; set; } = null!;

        [Required(ErrorMessage = emailIsRequired)]
        [EmailAddress(ErrorMessage = notValidEmail)]
        [MaxLength(emailMaxLength, ErrorMessage = emailTooLongErrorMessage)]
        public string Email { get; set; } = null!;
    }
}
