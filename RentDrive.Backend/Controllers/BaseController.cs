using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Enums;
using RentDrive.Web.ViewModels.VehicleTypeProperty;
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
        public IActionResult GetFuelTypesEnum()
        {
            IEnumerable<FuelTypeEnumViewModel> fuelTypes = this.baseService
                .GetFuelTypesEnum();

            return Ok(fuelTypes);
        }
        [HttpGet("value-types")]
        public IActionResult GetValueTypesEnum()
        {
            IEnumerable<ValueTypeViewModel> valueTypes = this.baseService
                .GetValueTypesEnum();

            return Ok(valueTypes);
        }
        [HttpGet("units")]
        public IActionResult GetUnitsEnum()
        {
            IEnumerable<UnitOfMeasurementViewModel> units = this.baseService
                .GetUnitsEnum();

            return Ok(units);
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
