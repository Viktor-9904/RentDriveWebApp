using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleType;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : BaseController
    {
        private readonly IVehicleTypeService vehicleTypeService;
        public VehicleTypeController(
            IVehicleTypeService vehicleTypeService,
            IBaseService baseService) : base(baseService)
        {
            this.vehicleTypeService = vehicleTypeService;
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            ServiceResponse<IEnumerable<VehicleTypeViewModel>> response = await this.vehicleTypeService
                .GetAllVehicleTypesAsync();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleType([FromBody] VehicleTypeCreateFormViewModel viewModel)
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

            ServiceResponse<VehicleTypeCreateFormViewModel?> response = await this.vehicleTypeService
                .CreateNewVehicleType(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditVehicleType(int id, [FromBody] VehicleTypeEditFormViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest("ID mismatch");
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

            ServiceResponse<VehicleTypeEditFormViewModel?> response = await this.vehicleTypeService
                .EditVehicleType(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleTypeById(int id)
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

            ServiceResponse<bool> response = await this.vehicleTypeService
                .DeleteVehicleTypeByIdAsync(guidUserId, id);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
