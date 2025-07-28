using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;
using System.Formats.Asn1;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        public readonly IRentalService rentalService;

        public RentalController(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetBookedDatesByVehicleId(Guid vehicleId)
        {
            IEnumerable<DateTime> bookedDates = await this.rentalService
                .GetBookedDatesByVehicleIdAsync(vehicleId);

            return Ok(bookedDates);
        }
        [HttpPost("rent/{vehicleId}")]
        public async Task<IActionResult> RentVehicle(Guid vehicleId, RentVehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasVehicleBooked = await this.rentalService
                .RentVehicle(vehicleId, viewModel.RenterId, viewModel.BookedDates);

            if (!wasVehicleBooked)
            {
                return BadRequest("Failed to rent vehicle");
            }

            return Ok();
        }
        [HttpGet("my-rentals")]
        public async Task<IActionResult> GetUserRentals()
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

            IEnumerable<UserRentalViewModel> myRentals = await this.rentalService
                .GetUserRentalsByIdAsync(userId);

            return Ok(myRentals);
        }
        [HttpPost("confirm-rental/{rentalId}")]
        public async Task<IActionResult> ConfirmRentalById(Guid rentalId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool confirmedRental = await this.rentalService
                .ConfirmRentalByIdAsync(userId, rentalId);

            if (!confirmedRental)
            {
                return BadRequest("Rental confirmation failed.");
            }

            return Ok("Rental confirmed successfully.");
        }
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetUserVehiclesRentalsByVehicleId(Guid vehicleId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            IEnumerable<UserVehicleRentalViewModel> userVehicles = await this.rentalService
                .GetUserVehiclesRentals(userId, vehicleId);

            return Ok(userVehicles);
        }
    }
}
