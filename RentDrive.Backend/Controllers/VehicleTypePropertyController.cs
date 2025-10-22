using Azure;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeProperty;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypePropertyController : BaseController
    {
        private readonly IVehicleTypePropertyService vehicleTypePropertyService;
        public VehicleTypePropertyController(
            IVehicleTypePropertyService vehicleTypePropertyService,
            IBaseService baseService) : base(baseService)
        {
            this.vehicleTypePropertyService = vehicleTypePropertyService;
        }
        [HttpGet("types/properties")]
        public async Task<IActionResult> GetAllVehicleTypeProperties()
        {
            ServiceResponse<IEnumerable<VehicleTypePropertyViewModel>> response = await this.vehicleTypePropertyService
                .GetAllVehicleTypePropertiesAsync();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypePropertyViewModel viewModel)
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

            ServiceResponse<VehicleTypePropertyViewModel> response = await this.vehicleTypePropertyService
                .CreateVehicleTypeProperty(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPropertyById([FromBody] EditVehicleTypePropertyViewModel viewModel)
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

            ServiceResponse<bool> response = await this.vehicleTypePropertyService
                .EditPropertyAsync(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePropertyById(Guid id)
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

            ServiceResponse<bool> response = await this.vehicleTypePropertyService
                .DeletePropertyByIdAsync(guidUserId, id);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}