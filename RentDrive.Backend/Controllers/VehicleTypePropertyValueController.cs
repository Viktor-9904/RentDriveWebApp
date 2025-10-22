using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Common;
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
            ServiceResponse<FilterTypeProperties?> response = await this.vehicleTypePropertyValueService
                .LoadTypePropertyValuesByTypeIdAsync(vehicleTypeId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
