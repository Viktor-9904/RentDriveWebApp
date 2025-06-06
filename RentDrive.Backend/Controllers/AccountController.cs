using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.ApplicationUser;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
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

            IdentityResult result = await accountService.RegisterUserAsync(viewModel);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

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
    }
}
