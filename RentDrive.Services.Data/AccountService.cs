using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;

using static RentDrive.Common.EntityValidationConstants.RentalValidationConstans.Fees;

namespace RentDrive.Services.Data
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;
        private readonly IRepository<Rental, Guid> rentalRepository;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly IVehicleService vehicleService;
        private readonly IRentalService rentalService;

        public AccountService(
            IRepository<ApplicationUser, Guid> applicationUserRepositor,
            IRepository<Rental, Guid> rentalRepository,
            IRepository<ChatMessage, Guid> chatMessageRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IVehicleService vehicleService,
            IRentalService rentalService)
        {
            this.applicationUserRepository = applicationUserRepositor;
            this.rentalRepository = rentalRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.vehicleService = vehicleService;
            this.rentalService = rentalService;
        }
        public async Task<ServiceResponse<IdentityResult>> RegisterUserAsync(RegisterUserInputViewModel viewModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };

            IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

            if (!result.Succeeded)
            {
                return ServiceResponse<IdentityResult>.Fail("Failed To Create User!");
            }

            return ServiceResponse<IdentityResult>.Ok(result);
        }

        public async Task<ServiceResponse<SignInResult>> LoginUserAsync(string emailOrUsername, string password)
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
                return ServiceResponse<SignInResult>.Fail("Failed To Login!");
            }

            SignInResult result = await this.signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return ServiceResponse<SignInResult>.Fail("Failed To Login!");
            }

            return ServiceResponse<SignInResult>.Ok(result);
        }

        public async Task LogoutUserAsync()
        {
           await this.signInManager.SignOutAsync();
        }

        public async Task<ServiceResponse<OverviewDetailsViewModel?>> GetOverviewDetailsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ServiceResponse<OverviewDetailsViewModel?>.Fail("User Not Found!");
            }

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

            ServiceResponse<int> listedVehicleCountResponse = await this.vehicleService
                .GetUserListedVehicleCountAsync(user.Id);

            if (!listedVehicleCountResponse.Success)
            {
                return ServiceResponse<OverviewDetailsViewModel?>.Fail("Failed To Get User Listed Vehicle Count!");
            }

            overviewDetails.VehiclesListedCount = listedVehicleCountResponse.Result;
            //overviewDetails.UserRating = TODO

            return ServiceResponse<OverviewDetailsViewModel?>.Ok(overviewDetails);
        }

        public async Task<ServiceResponse<UserProfileDetailsViewModel?>> GetUserProfileDetailsByIdAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ServiceResponse<UserProfileDetailsViewModel?>.Fail("User Not Found!");
            }

            UserProfileDetailsViewModel userDetails = new UserProfileDetailsViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return ServiceResponse<UserProfileDetailsViewModel?>.Ok(userDetails);
        }

        public async Task<ServiceResponse<UserProfileDetailsViewModel?>> UpdateUserProfileDetails(string userId, UserProfileDetailsViewModel viewModel)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ServiceResponse<UserProfileDetailsViewModel?>.Fail("User Not Found!");
            }

            user.UserName = viewModel.Username;
            user.Email = viewModel.Email;
            user.PhoneNumber = viewModel.PhoneNumber;

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return ServiceResponse<UserProfileDetailsViewModel?>.Fail("Failed To Update User Profile!");
            }

            UserProfileDetailsViewModel userDetails = new UserProfileDetailsViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return ServiceResponse<UserProfileDetailsViewModel?>.Ok(userDetails);
        }

        public async Task<ServiceResponse<bool>> UpdatedUserPasswordAsync(string userId, UserChangePasswordInpuViewModel viewModel)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ServiceResponse<bool>.Fail("User Not Found!");
            }

            IdentityResult result = await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

            if (!result.Succeeded)
            {
                return ServiceResponse<bool>.Fail("Failed To Change Password!");
            }

            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<UserCredentialsViewModel?>> GetUserCredentialsByIdAsync(Guid userId)
        {
            ApplicationUser? user = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .Include(au => au.Rentals)
                .FirstOrDefaultAsync(au => au.Id == userId);

            if (user == null)
            {
                return ServiceResponse<UserCredentialsViewModel?>.Fail("User Not Found!");
            }

            decimal pendingBalance = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Vehicle)
                .Where(r =>
                    r.Vehicle.OwnerId == userId &&
                    r.Status == RentalStatus.Active)
                .Select(r => r.TotalPrice)
                .SumAsync()
                * (1 - CompanyPercentageFee);

            UserCredentialsViewModel userCredentials = new UserCredentialsViewModel()
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                MemberSince = user.CreatedOn,
                IsCompanyEmployee = user.UserType == UserType.CompanyEmployee,
                Balance = user.Wallet?.Balance ?? 0m,
                PendingBalance = pendingBalance
            };

            return ServiceResponse<UserCredentialsViewModel?>.Ok(userCredentials);
        }

        public async Task<ServiceResponse<bool>> Exists(string userId)
        {
            bool isGuidValid = Guid.TryParse(userId, out Guid guidId);
            if (!isGuidValid)
            {
                return ServiceResponse<bool>.Fail("Invalid User Id!");
            }

            return ServiceResponse<bool>.Ok(
                await this.applicationUserRepository
                .GetAllAsQueryable()
                .AnyAsync(au => au.Id == guidId));
        }
    }
}
