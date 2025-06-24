using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyService : IVehicleTypePropertyService
    {
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        public VehicleTypePropertyService(IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository)
        {
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
        }

        public async Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync()
        {
            IEnumerable<VehicleTypePropertyViewModel> vehicleTypeProperties = await this.vehicleTypePropertyRepository
                .GetAllAsQueryable()
                .Select(vtp => new VehicleTypePropertyViewModel()
                {
                    Id = vtp.Id,
                    VehicleTypeId = vtp.VehicleTypeId,
                    Name = vtp.Name,
                    ValueType = vtp.ValueType.ToString(),
                    UnitOfMeasurement = vtp.UnitOfMeasurement.ToString(),
                })
                .ToListAsync();

            return vehicleTypeProperties;
        }
    }
}
