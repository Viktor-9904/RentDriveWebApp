using Azure.Identity;
using Microsoft.AspNetCore.Identity;

using RentDrive.Data.Models;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Services.Data
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IVehicleService vehicleService;
        private readonly IRentalService rentalService;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IVehicleService vehicleService,
            IRentalService rentalService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.vehicleService = vehicleService;
            this.rentalService = rentalService;
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
        public async Task<SignInResult> LoginUserAsync(string emailOrUsername, string password)
        {
            ApplicationUser? user;

            if (emailOrUsername.Contains('@'))
            {
                user = await this.userManager.FindByEmailAsync(emailOrUsername);
            }
            else
            {
                user = await this.userManager.FindByNameAsync(emailOrUsername);
            }

            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await this.signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
        }

        public async Task LogoutUserAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            ApplicationUser? user =  await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<OverviewDetailsViewModel> GetOverviewDetailsByUserIdAsync(ApplicationUser user)
        {
            OverviewDetailsViewModel overviewDetails = new OverviewDetailsViewModel()
            {
                //TODO: add first and last names to overview after also adding them to register.
                Username = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber ?? "N/A",
                MemberSince = user.CreatedOn
            };

            overviewDetails.CompletedRentalsCount = await this.rentalService
                .GetCompletedRentalsCountByUserIdAsync(user.Id);

            overviewDetails.VehiclesListedCount = await this.vehicleService
                .GetUserListedVehicleCountAsync(user.Id);

            //overviewDetails.UserRating = TODO

            return overviewDetails;
        }
    }
}
