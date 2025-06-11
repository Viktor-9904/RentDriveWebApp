using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet("recent", Name = "RecentVehicles")]
        public async Task<IEnumerable<RecentVehicleIndexViewModel>> GetRecentVehicles()
        {
            IEnumerable<RecentVehicleIndexViewModel> recentVehiclesViewModels
                = await this.vehicleService.IndexGetTop3RecentVehiclesAsync();

            return recentVehiclesViewModels;
        }
        [HttpGet("all", Name = "GetAllVehicles")]
        public async Task<IEnumerable<ListingVehicleViewModel>> GetAllVehicles()
        {
            IEnumerable<ListingVehicleViewModel> allVehiclesViewModels 
                = await this.vehicleService.GetAllVehiclesAsync();

            return allVehiclesViewModels;
        }
    }
}
