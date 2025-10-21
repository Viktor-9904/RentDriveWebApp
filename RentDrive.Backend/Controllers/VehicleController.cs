using Azure;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : BaseController
    {
        private readonly IVehicleService vehicleService;
        public VehicleController(
            IVehicleService vehicleService,
            IBaseService baseService) : base(baseService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentVehicles()
        {
            ServiceResponse<IEnumerable<RecentVehicleIndexViewModel>> response = await this.vehicleService
                .IndexGetTop3RecentVehiclesAsync();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllVehicles()
        {
            ServiceResponse<IEnumerable<ListingVehicleViewModel>> response = await this.vehicleService
                .GetAllVehiclesAsync();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("user-vehicles")]
        public async Task<IActionResult> GetUserVehicles()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Unauthorized User!");
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(userId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User");
            }

            ServiceResponse<IEnumerable<UserVehicleViewModel>> response = await this.vehicleService
                .GetUserVehiclesByIdAsync(guidUserId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleDetailsById(string id)
        {
            Guid vehicleGuidId = Guid.Empty;
            bool isVehicleIdValid = IsGuidValid(id, ref vehicleGuidId);

            if (!isVehicleIdValid)
            {
                return NotFound("Invalid Vehicle Id!");
            }

            ServiceResponse<VehicleDetailsViewModel?> response = await this.vehicleService
                .GetVehicleDetailsByIdAsync(vehicleGuidId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("edit/{vehicleId}")]
        public async Task<IActionResult> GetEditVehicleDetailsById(Guid vehicleId)
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

            ServiceResponse<VehicleEditFormViewModel?> response = await this.vehicleService
                .GetEditVehicleDetailsByIdAsync(guidUserId, vehicleId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateVehicle(Guid id, [FromForm] VehicleEditFormViewModel viewModel) //TODO: Fix updating vehicle when older vehicle does not contain a newly created vehicleTypeProperty.
        {
            if (id != viewModel.Id)
            {
                return BadRequest("Mismatched Vehicle Id!");
            }

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

            ServiceResponse<bool> response = await this.vehicleService
                .UpdateVehicle(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] VehicleCreateFormViewModel viewModel)
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

            Guid userGuidId = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuidId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<bool> response = await this.vehicleService
                .CreateVehicle(userGuidId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
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

            ServiceResponse<bool> response = await this.vehicleService
                .SoftDeleteVehicleByIdAsync(guidUserId, id);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("base-filter-properties")]
        public async Task<IActionResult> GetBaseFilterProperties([FromQuery] int? vehicleTypeId = null, [FromQuery] int? vehicleTypeCategoryId = null)
        {
            ServiceResponse<BaseFilterProperties> response = await vehicleService
                .GetBaseFilterPropertiesAsync(vehicleTypeId, vehicleTypeCategoryId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterVehicles([FromBody] FilteredVehiclesViewModel filter)
        {
            if (filter == null)
                return BadRequest("Filter is required!");

            ServiceResponse<IEnumerable<Guid>> response = await vehicleService
                .GetFilteredVehicles(filter);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("search-vehicles")]
        public async Task<IActionResult> SearchVehicles([FromQuery] string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return BadRequest("Search query is required!");

            ServiceResponse<IEnumerable<ListingVehicleViewModel>> response = await vehicleService
                .GetSearchQueryVehicles(searchQuery);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("all-makes")]
        public async Task<IActionResult> GetAllMakes()
        {
            ServiceResponse<IEnumerable<string>> response = await this.vehicleService
                .GetAllVehicleMakesAsync();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("active-listings")]
        public async Task<IActionResult> GetActiveListings()
        {
            ServiceResponse<int> response = await this.vehicleService.GetActiveListings();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}