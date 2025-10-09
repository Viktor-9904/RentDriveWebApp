using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;
using RentDrive.Web.ViewModels.Chat;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(
            IAccountService accountService,
            IBaseService baseService) : base(baseService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register", Name = "Register User")]
        public async Task<IActionResult> Register([FromBody] RegisterUserInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult registerUser = await accountService.RegisterUserAsync(viewModel);

            if (!registerUser.Succeeded)
            {
                foreach (IdentityError error in registerUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            var loginUser = await this.accountService.LoginUserAsync(viewModel.Email, viewModel.Password);

            if (!loginUser.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "User was successfully registered but could not be logged in.");
                return BadRequest(ModelState);
            }

            return Ok("Successful registration.");
        }
        [HttpPost("login", Name = "Login User")]
        public async Task<IActionResult> Login([FromBody] LoginUserInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.accountService.LoginUserAsync(viewModel.EmailOrUsername, viewModel.Password);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok("Logged in successfully!");
        }
        [HttpPost("logout", Name = "Logout User")]
        public async Task<IActionResult> Logout()
        {
            await this.accountService.LogoutUserAsync();
            return Ok("User logged out");
        }
        [HttpGet("me", Name = "User credentials")]
        public async Task<IActionResult> GetUser()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            UserCredentialsViewModel? userCredentials = await this.accountService
                .GetUserCredentialsByIdAsync(userId);

            if (userCredentials == null)
            {
                return NotFound();
            }

            return Ok(userCredentials);
        }
        [HttpGet("overview-details", Name = "User overview")]
        public async Task<IActionResult> GetOverviewDetails()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            OverviewDetailsViewModel? overviewDetails = await this.accountService
                .GetOverviewDetailsByUserIdAsync(userId);

            if (overviewDetails == null)
            {
                return NotFound();
            }

            return Ok(overviewDetails);
        }
        [HttpGet("user-profile-details")]
        public async Task<IActionResult> GetUserProfileDetails()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            UserProfileDetailsViewModel? userDetails  = await this.accountService
                .GetUserProfileDetailsByIdAsync(userId);

            if (userDetails == null)
            {
                return BadRequest();
            }

            return Ok(userDetails);
        }
        [HttpPut("update-profile-details")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserProfileDetailsViewModel viewModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            UserProfileDetailsViewModel? userDetails = await this.accountService
                .UpdateUserProfileDetails(userId, viewModel);

            if (userDetails == null)
            {
                return BadRequest();
            }

            return Ok(userDetails);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserChangePasswordInpuViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            bool updatedPassword = await this.accountService
                .UpdatedUserPasswordAsync(userId, viewModel);

            if (!updatedPassword)
            {
                return BadRequest("Failed to update password.");
            }

            return Ok("Successfully updated password.");
        }
    }
}