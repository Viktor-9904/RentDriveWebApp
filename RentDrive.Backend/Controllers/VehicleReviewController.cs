using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;

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
    }
}
