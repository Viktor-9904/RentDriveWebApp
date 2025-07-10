using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeCategory;
using System.Threading.Tasks;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeCategoryController : ControllerBase
    {
        private readonly IVehicleTypeCategoryService vehicleTypeCategoryService;

        public VehicleTypeCategoryController(IVehicleTypeCategoryService vehicleTypeCategoryService)
        {
            this.vehicleTypeCategoryService = vehicleTypeCategoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllVehicleTypeCategories()
        {
            IEnumerable<VehicleTypeCategoryViewModel> allCategories = await this.vehicleTypeCategoryService
                .GetAllCategories();

            if (allCategories == null)
            {
                return NotFound();
            }

            return Ok(allCategories);
        }
    }
}
