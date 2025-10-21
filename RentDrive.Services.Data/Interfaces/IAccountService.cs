using Microsoft.AspNetCore.Identity;

using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse<IdentityResult>> RegisterUserAsync(RegisterUserInputViewModel viewModel);
        public Task<ServiceResponse<SignInResult>> LoginUserAsync(string emailOrUsername, string password);
        public Task LogoutUserAsync();
        public Task<ServiceResponse<OverviewDetailsViewModel?>> GetOverviewDetailsByUserIdAsync(string userId);
        public Task<ServiceResponse<UserProfileDetailsViewModel?>> GetUserProfileDetailsByIdAsync(string userId);
        public Task<ServiceResponse<UserProfileDetailsViewModel?>> UpdateUserProfileDetails(string userId, UserProfileDetailsViewModel viewModel);
        public Task<ServiceResponse<bool>> UpdatedUserPasswordAsync(string userId, UserChangePasswordInpuViewModel viewModel);
        public Task<ServiceResponse<UserCredentialsViewModel?>> GetUserCredentialsByIdAsync(Guid userId);
        public Task<ServiceResponse<bool>> Exists(string userId);
    }
}
