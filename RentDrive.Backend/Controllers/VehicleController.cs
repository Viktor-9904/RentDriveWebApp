using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RentDrive.Services.Data;
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
        public async Task<IEnumerable<RecentVehicleIndexViewModel>> GetRecentVehicles()
        {
            IEnumerable<RecentVehicleIndexViewModel> recentVehiclesViewModels
                = await this.vehicleService.IndexGetTop3RecentVehiclesAsync();

            return recentVehiclesViewModels;
        }
        [HttpGet("all")]
        public async Task<IEnumerable<ListingVehicleViewModel>> GetAllVehicles()
        {
            IEnumerable<ListingVehicleViewModel> allVehiclesViewModels = await this.vehicleService
                .GetAllVehiclesAsync();

            return allVehiclesViewModels;
        }
        [HttpGet("user-vehicles")]
        public async Task<IActionResult> GetUserVehicles()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            IEnumerable<UserVehicleViewModel> userVehicles = await this.vehicleService
                .GetUserVehiclesByIdAsync(userId);

            return Ok(userVehicles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleDetailsById(string id)
        {
            Guid vehicleGuidId = Guid.Empty;
            bool isVehicleIdValid = IsGuidValid(id, ref vehicleGuidId);

            if (!isVehicleIdValid)
            {
                return NotFound();
            }

            VehicleDetailsViewModel? vehicleDetails = await this.vehicleService
                .GetVehicleDetailsByIdAsync(vehicleGuidId);

            if (vehicleDetails == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicleDetails);
        }
        [HttpGet("edit/{vehicleId}")]
        public async Task<IActionResult> GetEditVehicleDetailsById(Guid vehicleId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(userId, ref guidUserId))
            {
                return BadRequest();
            }

            VehicleEditFormViewModel? vehicleDetails = await this.vehicleService
                .GetEditVehicleDetailsByIdAsync(guidUserId, vehicleId);

            if (vehicleDetails == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicleDetails);
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateVehicle(Guid id, [FromForm] VehicleEditFormViewModel viewModel) //TODO: Fix updating vehicle when older vehicle does not contain a newly created vehicleTypeProperty.
        {
            if (id != viewModel.Id)
            {
                return BadRequest("Mismatched ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(userId, ref guidUserId))
            {
                return BadRequest();
            }

            bool wasVehicleUpdated = await this.vehicleService
                .UpdateVehicle(guidUserId, viewModel);

            if (!wasVehicleUpdated)
            {
                return BadRequest("Failed to update Vehicle.");
            }

            return Ok();
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
                return Unauthorized();
            }

            bool wasVehicleCreated = await this.vehicleService
                .CreateVehicle(userId, viewModel);

            if (!wasVehicleCreated)
            {
                return BadRequest("Failed to create Vehicle.");
            }

            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool wasVehicleSuccessfullySoftDeleted = await this.vehicleService
                .SoftDeleteVehicleByIdAsync(id);

            if (!wasVehicleSuccessfullySoftDeleted)
            {
                return BadRequest("Failed to remove Vehicle");
            }

            return Ok();
        }
        [HttpGet("base-filter-properties")]
        public async Task<IActionResult> GetBaseFilterProperties([FromQuery] int? vehicleTypeId = null, [FromQuery] int? vehicleTypeCategoryId = null)
        {
            BaseFilterProperties baseProperties = await vehicleService.GetBaseFilterPropertiesAsync(vehicleTypeId, vehicleTypeCategoryId);

            return Ok(baseProperties);
        }
        [HttpPost("filter")]
        public async Task<IActionResult> FilterVehicles([FromBody] FilteredVehiclesViewModel filter)
        {
            if (filter == null)
                return BadRequest("Filter is required.");

            IEnumerable<Guid> vehicles = await vehicleService
                .GetFilteredVehicles(filter);

            return Ok(vehicles);
        }
        [HttpGet("search-vehicles")]
        public async Task<IActionResult> SearchVehicles([FromQuery] string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return BadRequest("Search query is required.");

            IEnumerable<ListingVehicleViewModel> vehicles = await vehicleService
                .GetSearchQueryVehicles(searchQuery);

            return Ok(vehicles);
        }
        [HttpGet("all-makes")]
        public async Task<IActionResult> GetAllMakes()
        {
            IEnumerable<string> allMakes = await this.vehicleService
                .GetAllVehicleMakesAsync();

            return Ok(allMakes);
        }
        [HttpGet("active-listings")]
        public async Task<IActionResult> GetActiveListings()
        {
            return Ok(await this.vehicleService.GetActiveListings());
        }
    }
}