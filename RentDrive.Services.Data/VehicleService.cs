using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Services.Data
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        private readonly IVehicleTypeService vehicleTypeService;
        private readonly IVehicleImageService vehicleImageService;
        private readonly IVehicleTypePropertyService vehicleTypePropertyService;
        private readonly IVehicleTypePropertyValueService vehicleTypePropertyValueService;
        public VehicleService(
            IRepository<Vehicle, Guid> vehicleRepository,
            IVehicleTypeService vehicleTypeService,
            IVehicleImageService vehicleImageService,
            IVehicleTypePropertyService vehicleTypePropertyService,
            IVehicleTypePropertyValueService vehicleTypePropertyValueService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleTypeService = vehicleTypeService;
            this.vehicleImageService = vehicleImageService;
            this.vehicleTypePropertyService = vehicleTypePropertyService;
            this.vehicleTypePropertyValueService = vehicleTypePropertyValueService;
        }

        public async Task<IEnumerable<ListingVehicleViewModel>> GetAllVehiclesAsync()
        {
            IEnumerable<ListingVehicleViewModel> allVehicles = await this.vehicleRepository
                .GetAllAsQueryable()
                .OrderBy(v => v.Make)
                .ThenBy(v => v.Model)
                .Select(v => new ListingVehicleViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    VehicleType = v.VehicleType.Name,
                    VehicleTypeCategory = v.VehicleTypeCategory.Name,
                    YearOfProduction = v.DateOfProduction.Year,
                    PricePerDay = v.PricePerDay,
                    //FuelType = v.FuelType.ToString(),
                    OwnerName = v.Owner.UserName
                })
                .ToListAsync();

            foreach (ListingVehicleViewModel vehicle in allVehicles)
            {
                string currentVehicleImageURL = await this.vehicleImageService.GetFirstImageByVehicleIdAsync(vehicle.Id);
                vehicle.ImageURL = currentVehicleImageURL;
            }

            return allVehicles;
        }

        public async Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync()
        {
            IEnumerable<RecentVehicleIndexViewModel> top3RecentVehicles = await this.vehicleRepository
                .GetAllAsQueryable()
                .OrderByDescending(v => v.DateAdded)
                .ThenBy(v => v.Make)
                .ThenBy(v => v.Model)
                .Take(3)
                .Select(v => new RecentVehicleIndexViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    PricePerDay = v.PricePerDay,
                    ImageURL = DefaultImageURL,
                    OwnerId = v.OwnerId,
                    OwnerName = v.Owner.UserName,
                    YearOfProduction = v.DateOfProduction.Year,
                    //FuelType = v.FuelType.ToString(),
                    Description = v.Description,
                    VehicleType = v.VehicleType.Name,
                    VehicleTypeCategory = v.VehicleTypeCategory.Name,
                })
                .ToArrayAsync();

            foreach (RecentVehicleIndexViewModel vehicle in top3RecentVehicles)
            {
                string currentVehicleImageURL = await this.vehicleImageService.GetFirstImageByVehicleIdAsync(vehicle.Id);
                vehicle.ImageURL = currentVehicleImageURL;
            }

            return top3RecentVehicles;
        }
        public async Task<VehicleDetailsViewModel?> GetVehicleDetailsByIdAsync(Guid id)
        {
            VehicleDetailsViewModel? vehicleDetails = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.Id == id)
                .Select(v => new VehicleDetailsViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    OwnerName = v.Owner.UserName,
                    VehicleType = v.VehicleType.Name,
                    VehicleTypeCategory = v.VehicleTypeCategory.Name,
                    Color = v.Color,
                    PricePerDay = v.PricePerDay,
                    DateOfProduction = v.DateOfProduction,
                    DateAdded = v.DateAdded,
                    CurbWeightInKg = v.CurbWeightInKg,
                    //OdoKilometers = v.OdoKilometers,
                    //EngineDisplacement = v.EngineDisplacement,
                    FuelType = v.FuelType.ToString(),
                    Description = v.Description,
                    //PowerInKiloWatts = v.PowerInKiloWatts,
                })
                .FirstOrDefaultAsync();

            if (vehicleDetails == null)
            {
                return null;
            }

            vehicleDetails.ImageURLS = await this.vehicleImageService
                .GetAllImagesByVehicleIdAsync(id);

            vehicleDetails.VehicleProperties = await this.vehicleTypePropertyValueService
                .GetVehicleTypePropertyValuesByVehicleIdAsync(id);

            return vehicleDetails;
        }
        public async Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypes()
        {
            return await this.vehicleTypeService
                .GetAllVehicleTypesAsync();
        }

        public async Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync()
        {
            return await this.vehicleTypePropertyService
                .GetAllVehicleTypePropertiesAsync();
        }
    }
}
