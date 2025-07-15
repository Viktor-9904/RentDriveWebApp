using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleType;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService vehicleTypeService;
        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            this.vehicleTypeService = vehicleTypeService;
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            IEnumerable<VehicleTypeViewModel> vehicleTypes = await this.vehicleTypeService
                .GetAllVehicleTypesAsync();

            if (vehicleTypes == null)
            {
                return NotFound();
            }

            return Ok(vehicleTypes);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleTypeById(int id)
        {
            bool vehicleTypeSuccsessfullyDeleted = await this.vehicleTypeService
                .DeleteVehicleTypeByIdAsync(id);

            if (!vehicleTypeSuccsessfullyDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditVehicleType(int id, [FromBody] VehicleTypeEditFormViewModel viewModel)
        {

            if (id != viewModel.Id)
            {
                return BadRequest("ID mismatch");
            }

            VehicleTypeEditFormViewModel? editedVehicleType = await this.vehicleTypeService
                .EditVehicleType(viewModel);

            if (editedVehicleType == null)
            {
                return BadRequest();
            }

            return Ok(editedVehicleType);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleType([FromBody] VehicleTypeCreateFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleTypeCreateFormViewModel? newVehicleType = await this.vehicleTypeService
                .CreateNewVehicleType(viewModel);

            if (newVehicleType == null)
            {
                return BadRequest();
            }

            return Ok(newVehicleType);
        }
    }
}
