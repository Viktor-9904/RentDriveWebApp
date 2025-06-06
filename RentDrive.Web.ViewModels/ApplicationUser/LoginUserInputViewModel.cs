using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.ErrorValidationMessages.ApplicationUserErrorMessages.LoginUser;

namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public  class LoginUserInputViewModel
    {
        [Required(ErrorMessage = emailOrUsernameIsRequired)]
        public string EmailOrUsername { get; set; } = null!;

        [Required(ErrorMessage = passwordIsRequired)]
        public string Password { get; set; } = null!;
    }
}
