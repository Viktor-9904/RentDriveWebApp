using Microsoft.AspNetCore.Identity;
using RentDrive.Data.Models;
using RentDrive.Web.ViewModels.ApplicationUser;
using System.Globalization;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterUserInputViewModel viewModel);
        public Task<SignInResult> LoginUserAsync(string emailOrUsername, string password);
        public Task LogoutUserAsync();
        public Task<ApplicationUser?> GetUserByIdAsync(string id);
        public Task<OverviewDetailsViewModel?> GetOverviewDetailsByUserIdAsync(string userId);
        public Task<UserProfileDetailsViewModel?> GetUserProfileDetailsByIdAsync(string userId);
        public Task<UserProfileDetailsViewModel?> UpdateUserProfileDetails(string userId, UserProfileDetailsViewModel viewModel);
        public Task<bool> UpdatedUserPasswordAsync(string userId, UserChangePasswordInpuViewModel viewModel);
        public Task<UserCredentialsViewModel?> GetUserCredentialsByIdAsync(string userId);
        public Task<bool> Exists(string userId);
    }
}
