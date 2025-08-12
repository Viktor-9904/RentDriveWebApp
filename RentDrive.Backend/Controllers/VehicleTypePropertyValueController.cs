using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypePropertyValueController : ControllerBase
    {
        private readonly IVehicleTypePropertyValueService vehicleTypePropertyValueService;

        public VehicleTypePropertyValueController(IVehicleTypePropertyValueService vehicleTypePropertyValueService)
        {
            this.vehicleTypePropertyValueService = vehicleTypePropertyValueService;
        }

        [HttpGet("filter/{vehicleTypeId}")]
        public async Task<IActionResult> GetTypePropertiesDetails(int vehicleTypeId)
        {
            FilterTypeProperties? filterTypeProperties = await this.vehicleTypePropertyValueService
                .LoadTypePropertyValuesByTypeIdAsync(vehicleTypeId);

            return Ok(filterTypeProperties);
        }
    }
}
