using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.WalletTransaction;
using System.Security.Claims;

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

            WalletTransactionHistoryViewModel? transaction = await this.walletService
                .AddFundsAsync(userId, viewModel);

            if (transaction == null)
            {
                return BadRequest("Failed to add funds to account.");
            }

            return Ok(transaction);
        }
    }
}
