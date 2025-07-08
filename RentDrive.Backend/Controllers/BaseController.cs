using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Enums;
using System.Threading.Tasks;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IBaseService baseService;
        public BaseController(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        [HttpGet("fuel-types")]
        public async Task<IActionResult> GetFuelTypeEnum()
        {
            IEnumerable<FuelTypeEnumViewModel> fuelTypes = this.baseService
                .GetFuelTypesEnum();

            return Ok(fuelTypes);
        }
        protected bool IsGuidValid(string id, ref Guid guidId)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            bool isGuidValid = Guid.TryParse(id, out guidId);
            if (!isGuidValid)
            {
                return false;
            }
            return true;
        }
    }
}
