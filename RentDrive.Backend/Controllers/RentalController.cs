using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;
using System.Formats.Asn1;

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
    }
}
