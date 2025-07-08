using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;

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
                return NotFound();
            }

            return Ok(vehicleDetails);
        }
    }
}