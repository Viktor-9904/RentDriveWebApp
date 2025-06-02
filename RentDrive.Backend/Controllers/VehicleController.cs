using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Backend.Controllers
{
    [Route("api/recent")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet(Name = "RecentVehicles")]
        public async Task<IEnumerable<RecentVehicleIndexViewModel>> GetRecentVehicles()
        {
            IEnumerable<RecentVehicleIndexViewModel> recentVehiclesViewModels
                = await this.vehicleService.IndexGetTop3RecentVehiclesAsync();

            return recentVehiclesViewModels;
        }
    }
}
