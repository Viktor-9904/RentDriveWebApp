using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : BaseController
    {
        public readonly IRentalService rentalService;

        public RentalController(
            IRentalService rentalService,
            IBaseService baseService) : base(baseService)
        {
            this.rentalService = rentalService;
        }
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetBookedDatesByVehicleId(Guid vehicleId)
        {
            ServiceResponse<IEnumerable<DateTime>> response = await this.rentalService
                .GetBookedDatesByVehicleIdAsync(vehicleId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("rent/{vehicleId}")]
        public async Task<IActionResult> RentVehicle(Guid vehicleId, RentVehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            ServiceResponse<bool> response = await this.rentalService
                .RentVehicle(vehicleId, guidUserId, viewModel.BookedDates);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("my-rentals")]
        public async Task<IActionResult> GetUserRentals()
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

            ServiceResponse<IEnumerable<UserRentalViewModel>> response = await this.rentalService
                .GetUserRentalsByIdAsync(guidUserId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("confirm-rental/{rentalId}")]
        public async Task<IActionResult> ConfirmRentalById(Guid rentalId)
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

            ServiceResponse<bool> response= await this.rentalService
                .ConfirmRentalByIdAsync(guidUserId, rentalId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("cancel-rental/{rentalId}")]
        public async Task<IActionResult> CancelRentalById(Guid rentalId)
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

            ServiceResponse<bool> response = await this.rentalService
                .CancelRentalByIdAsync(guidUserId, rentalId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetUserVehiclesRentalsByVehicleId(Guid vehicleId)
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

            ServiceResponse<IEnumerable<UserVehicleRentalViewModel>> response= await this.rentalService
                .GetUserOwnedVehiclesRentals(guidUserId, vehicleId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
