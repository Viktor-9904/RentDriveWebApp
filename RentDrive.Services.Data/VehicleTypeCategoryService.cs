using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeCategory;

namespace RentDrive.Services.Data
{
    public class VehicleTypeCategoryService : IVehicleTypeCategoryService
    {

        private readonly IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository;
        public VehicleTypeCategoryService(IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository)
        {
            this.vehicleTypeCategoryRepository = vehicleTypeCategoryRepository;
        }
        public async Task<IEnumerable<VehicleTypeCategoryViewModel>> GetAllCategories()
        {
            IEnumerable<VehicleTypeCategoryViewModel> allCategories = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .Select(vtc => new VehicleTypeCategoryViewModel()
                {
                    Id = vtc.Id,
                    VehicleTypeId = vtc.VehicleTypeId,
                    Name = vtc.Name,
                    Description = vtc.Description
                })
                .ToListAsync();

            return allCategories;
        }
    }
}
