using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.ApplicationUser;

namespace RentDrive.Common.ErrorValidationMessages
{
    public static class ApplicationUserErrorMessages
    {
        public static class ApplicationUser
        {
            public const string usernameIsRequired = "Username is required!";
            public const string usernameTooShortErrorMessage = "Username must be at least 3 characters long!";
            public const string usernameTooLongErrorMessage = "Username must not exceed 20 characters!";

            public const string passwordIsRequired = "Password is required!";
            public const string passwordTooShortErrorMessage = "Password must be at least 6 characters long!";
            public const string passwordTooLongErrorMessage = "Password must not exceed 50 characters!";

            public const string comfirmedPasswordIsRequired = "Comfirmed password is required!";
            public const string comfirmedPasswordDoesntMatch = "Password doesn't match!";

            public const string emailIsRequired = "Email is required!";
            public const string notValidEmail = "Email is not valid!";
            public const string emailTooLongErrorMessage = "Email must not exceed 254 characters!";
        }
    }
}
