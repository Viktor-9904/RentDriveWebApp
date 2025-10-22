using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.WalletTransaction;
using RentDrive.Services.Data.Interfaces;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost("add-funds")]
        public async Task<IActionResult> AddFunds(AddFundsViewModel viewModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResponse<WalletTransactionHistoryViewModel?> response = await this.walletService
                .AddFundsAsync(userId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
