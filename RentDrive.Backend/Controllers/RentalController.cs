using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
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
    }
}
