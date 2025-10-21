using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;
using System.Security.Claims;

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

            ServiceResponse<IdentityResult> registerResponse = await accountService.RegisterUserAsync(viewModel);

            if (!registerResponse.Success)
            {
                return BadRequest(registerResponse.ErrorMessage);
            }

            ServiceResponse<Microsoft.AspNetCore.Identity.SignInResult> loginResponse = await this.accountService.LoginUserAsync(viewModel.Email, viewModel.Password);

            if (!loginResponse.Success)
            {
                return BadRequest(loginResponse.ErrorMessage);
            }

            return Ok(loginResponse.Result);
        }

        [HttpPost("login", Name = "Login User")]
        public async Task<IActionResult> Login([FromBody] LoginUserInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResponse<Microsoft.AspNetCore.Identity.SignInResult> response = await this.accountService.LoginUserAsync(viewModel.EmailOrUsername, viewModel.Password);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
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
                return Unauthorized("Unauthorized User!");
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(userId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }


            ServiceResponse<UserCredentialsViewModel?> response = await this.accountService
                .GetUserCredentialsByIdAsync(guidUserId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("overview-details", Name = "User overview")]
        public async Task<IActionResult> GetOverviewDetails()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Unauthorized User!");
            }

           ServiceResponse<OverviewDetailsViewModel?> response = await this.accountService
                .GetOverviewDetailsByUserIdAsync(userId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("user-profile-details")]
        public async Task<IActionResult> GetUserProfileDetails()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

           ServiceResponse<UserProfileDetailsViewModel?> response = await this.accountService
                .GetUserProfileDetailsByIdAsync(userId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPut("update-profile-details")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserProfileDetailsViewModel viewModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            ServiceResponse<UserProfileDetailsViewModel?> response = await this.accountService
                .UpdateUserProfileDetails(userId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
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
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<bool> response = await this.accountService
                .UpdatedUserPasswordAsync(userId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}