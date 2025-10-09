using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;
using RentDrive.Web.ViewModels.Chat;
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
            ApplicationUser? user = await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<OverviewDetailsViewModel?> GetOverviewDetailsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
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

            overviewDetails.VehiclesListedCount = await this.vehicleService
                .GetUserListedVehicleCountAsync(user.Id);

            //overviewDetails.UserRating = TODO

            return overviewDetails;
        }

        public async Task<UserProfileDetailsViewModel?> GetUserProfileDetailsByIdAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            UserProfileDetailsViewModel userDetails = new UserProfileDetailsViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return userDetails;
        }

        public async Task<UserProfileDetailsViewModel?> UpdateUserProfileDetails(string userId, UserProfileDetailsViewModel viewModel)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            user.UserName = viewModel.Username;
            user.Email = viewModel.Email;
            user.PhoneNumber = viewModel.PhoneNumber;

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return null;
            }

            UserProfileDetailsViewModel userDetails = new UserProfileDetailsViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return userDetails;
        }

        public async Task<bool> UpdatedUserPasswordAsync(string userId, UserChangePasswordInpuViewModel viewModel)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            IdentityResult result = await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

            return result.Succeeded;
        }

        public async Task<UserCredentialsViewModel?> GetUserCredentialsByIdAsync(string userId)
        {

            ApplicationUser? user = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .Include(au => au.Rentals)
                .FirstOrDefaultAsync(au => au.Id.ToString() == userId);

            if (user == null)
            {
                return null;
            }

            decimal pendingBalance = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Vehicle)
                .Where(r =>
                    r.Vehicle.OwnerId.ToString() == userId &&
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

            return userCredentials;
        }

        public async Task<bool> Exists(string userId)
        {
            bool isGuid = Guid.TryParse(userId, out Guid guidId);
            if (!isGuid)
            {
                return false;
            }
            
            return await this.applicationUserRepository
                .GetAllAsQueryable()
                .AnyAsync(au => au.Id == guidId);
        }
    }
}
