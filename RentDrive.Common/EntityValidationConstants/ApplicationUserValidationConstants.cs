using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
