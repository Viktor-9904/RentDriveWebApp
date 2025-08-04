namespace RentDrive.Common.EntityValidationConstants
{
    public static class ApplicationUserValidationConstants
    {
        public static class ApplicationUser
        {
            // IMPORTANT: IF CHANGED, MAKE SURE TO UPDATE ERROR MESSAGES, CLIENT VALIDATION ERROR MESSAGES AND APPSETTINGS.JSON IDENTITY AS WELL!

            public const int usernameMinLength = 3;
            public const int usernameMaxLength = 20;

            public const int passwordMinLength = 6;
            public const int passwordMaxLength = 50;

            public const int emailMaxLength = 254;
        }
        public static class Company
        {
            public const string CompanyId = "807fafdb-d496-43c1-ae22-a0a0ead66653";
        }
    }
}
