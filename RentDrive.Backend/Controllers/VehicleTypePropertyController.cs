using Microsoft.AspNetCore.Mvc;
using RentDrive.Common.Enums;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeProperty;

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
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypePropertyViewModel viewModel)
        {

            ;
            return Ok();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPropertyById([FromBody] EditVehicleTypePropertyViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasPropertyUpdated = await this.vehicleTypePropertyService
                .EditPropertyAsync(viewModel);

            if (!wasPropertyUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpGet("value-and-unit-enums")]
        public IActionResult GetValueAndUnitEnums()
        {
            EnumOptionsViewModel enumOptions = this.vehicleTypePropertyService
                .GetEnumOptions();

            return Ok(enumOptions);
        }
    }
}
