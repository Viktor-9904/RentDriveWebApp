using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypePropertyController : ControllerBase
    {
        private readonly IVehicleTypePropertyService vehicleTypePropertyService;
        public VehicleTypePropertyController(IVehicleTypePropertyService vehicleTypePropertyService)
        {
            this.vehicleTypePropertyService = vehicleTypePropertyService;
        }
        [HttpGet("types/properties")]
        public async Task<IActionResult> GetAllVehicleTypeProperties()
        {
            IEnumerable<VehicleTypePropertyViewModel> vehicleTypeProperties = await this.vehicleTypePropertyService
                .GetAllVehicleTypePropertiesAsync();

            if (vehicleTypeProperties == null)
            {
                return NotFound();
            }

            return Ok(vehicleTypeProperties);
        }
    }
}
