using Azure;
using Microsoft.AspNetCore.Mvc;

using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeCategory;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeCategoryController : BaseController
    {
        private readonly IVehicleTypeCategoryService vehicleTypeCategoryService;

        public VehicleTypeCategoryController(
            IVehicleTypeCategoryService vehicleTypeCategoryService,
            IBaseService baseService) : base(baseService)
        {
            this.vehicleTypeCategoryService = vehicleTypeCategoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllVehicleTypeCategories()
        {
            ServiceResponse<IEnumerable<VehicleTypeCategoryViewModel>> response = await this.vehicleTypeCategoryService
                .GetAllCategories();

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteVehicleTypeCategoryById(int categoryId)
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

            ServiceResponse<bool> response = await this.vehicleTypeCategoryService
                .DeleteByIdAsync(guidUserId, categoryId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditVehicleTypeCategory(int id, VehicleTypeCategoryEditFormViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest("ID mismatch");
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

            ServiceResponse<VehicleTypeCategoryEditFormViewModel?> response = await this.vehicleTypeCategoryService
                .EditCategory(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleTypeCategory(VehicleTypeCategoryCreateFormViewModel viewModel)
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

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(userId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<VehicleTypeCategoryCreateFormViewModel?> response = await this.vehicleTypeCategoryService
                .CreateCategory(guidUserId, viewModel);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
