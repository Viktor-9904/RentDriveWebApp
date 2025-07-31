using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleReviewController : ControllerBase
    {
        private readonly IVehicleReviewService vehicleReviewService;

        public VehicleReviewController(IVehicleReviewService vehicleReviewService)
        {
            this.vehicleReviewService = vehicleReviewService;
        }

        [HttpGet("average-star-rating/{vehicleId}")]
        public async Task<IActionResult> GetAverageVehicleStarRatingById(Guid vehicleId)
        {
            return Ok(await this.vehicleReviewService.GetVehicleStarRatingByIdAsync(vehicleId));
        }
        [HttpGet("reviews-count/{vehicleId}")]
        public async Task<IActionResult> GetTotalReviewCountByVehicleId(Guid vehicleId)
        {
            return Ok(await this.vehicleReviewService.GetVehicleReviewCountByIdAsync(vehicleId));
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddVehicleReview([FromBody] AddNewReviewViewModel viewModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool AddedVehicleReview = await this.vehicleReviewService
                .AddVehicleReview(userId, viewModel);

            if (!AddedVehicleReview)
            {
                return BadRequest("Falied to add review.");
            }
            
            return Ok();
        }
    }
}
