using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

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
    }
}
