using Microsoft.AspNetCore.Identity;
using RentDrive.Data.Models;
using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterUserInputViewModel viewModel);
        public Task<SignInResult> LoginUserAsync(string emailOrUsername, string password);
        public Task LogoutUserAsync();
        public Task<ApplicationUser?> GetUserByIdAsync(string id);
        public Task<OverviewDetailsViewModel> GetOverviewDetailsByUserIdAsync(ApplicationUser user);
    }
}
