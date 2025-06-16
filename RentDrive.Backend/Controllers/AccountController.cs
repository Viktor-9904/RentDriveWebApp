using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Data.Models;
using RentDrive.Services.Data;
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

        public AccountController(IAccountService accountService)
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

            ApplicationUser? user = await this.accountService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(
                new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                });
        }
    }
}
