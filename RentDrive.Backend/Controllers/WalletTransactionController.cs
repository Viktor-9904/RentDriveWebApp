using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.WalletTransaction;
using System.Security.AccessControl;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletTransactionController : ControllerBase
    {
        private readonly IWalletTransaction walletTransactionService;

        public WalletTransactionController(IWalletTransaction walletTransactionService)
        {
            this.walletTransactionService = walletTransactionService;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUserWalletTransactionHistory()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            IEnumerable<WalletTransactionHistoryViewModel> transactions = await this.walletTransactionService
                .GetWalletTransactionHistoryByUserIdAsync(userId);

            return Ok(transactions);
        }
    }
}
