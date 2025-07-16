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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleTypeCategoryById(int id)
        {
            bool wasVehicleTypeCategorySuccessfullyDeleted = await this.vehicleTypeCategoryService
                .DeleteByIdAsync(id);

            if (!wasVehicleTypeCategorySuccessfullyDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
