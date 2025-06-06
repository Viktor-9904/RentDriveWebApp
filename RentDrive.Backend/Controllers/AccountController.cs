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
        private readonly IApplicationUserService userService;

        public AccountController(IApplicationUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register", Name = "Register User")]
        public async Task<IActionResult> Register([FromBody] RegisterUserInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await userService.RegisterUserAsync(viewModel);

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
    }
}
