using Microsoft.AspNetCore.Identity;

using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterUserInputViewModel viewModel);
        public Task<SignInResult> LoginUserAsync(string emailOrUsername, string password);
    }
}
