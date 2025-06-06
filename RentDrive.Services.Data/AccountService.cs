using Microsoft.AspNetCore.Identity;

using RentDrive.Data.Models;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Services.Data
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterUserInputViewModel viewModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };

            IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);
            return result;
        }
    }
}
