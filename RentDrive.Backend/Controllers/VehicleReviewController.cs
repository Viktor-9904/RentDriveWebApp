using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleReviewController : BaseController
    {
        private readonly IVehicleReviewService vehicleReviewService;

        public VehicleReviewController(
            IVehicleReviewService vehicleReviewService, 
            IBaseService baseService) : base(baseService)
        {
            this.vehicleReviewService = vehicleReviewService;
        }

        [HttpGet("average-star-rating/{vehicleId}")]
        public async Task<IActionResult> GetAverageVehicleStarRatingById(Guid vehicleId)
        {
            ServiceResponse<double> response = await this.vehicleReviewService
                .GetVehicleStarRatingByIdAsync(vehicleId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("reviews-count/{vehicleId}")]
        public async Task<IActionResult> GetTotalReviewCountByVehicleId(Guid vehicleId)
        {
            ServiceResponse<int> response = await this.vehicleReviewService
                .GetVehicleReviewCountByIdAsync(vehicleId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVehicleReview([FromBody] AddNewReviewViewModel viewModel)
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

            ServiceResponse<bool> response = await this.vehicleReviewService
                .AddVehicleReview(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
