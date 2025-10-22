using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletTransactionController : BaseController
    {
        private readonly IWalletTransaction walletTransactionService;

        public WalletTransactionController(
            IWalletTransaction walletTransactionService,
            IBaseService baseService) : base(baseService)
        {
            this.walletTransactionService = walletTransactionService;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUserWalletTransactionHistory()
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized("Unauthorized User!");
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(currentUserId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<IEnumerable<WalletTransactionHistoryViewModel>> response = await this.walletTransactionService
                .GetWalletTransactionHistoryByUserIdAsync(guidUserId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
