using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentDrive.Data.Migrations;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;
using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Services.Data
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        private readonly IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository;

        private readonly IBaseService baseService;
        private readonly IVehicleImageService vehicleImageService;
        private readonly IVehicleTypePropertyService vehicleTypePropertyService;
        private readonly IVehicleTypePropertyValueService vehicleTypePropertyValueService;
        public VehicleService(
            IRepository<Vehicle, Guid> vehicleRepository,
            IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository,
            IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository,
            IBaseService baseService,
            IVehicleImageService vehicleImageService,
            IVehicleTypePropertyService vehicleTypePropertyService,
            IVehicleTypePropertyValueService vehicleTypePropertyValueService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
            this.vehicleTypePropertyValueRepository = vehicleTypePropertyValueRepository;
            this.baseService = baseService;
            this.vehicleImageService = vehicleImageService;
            this.vehicleTypePropertyService = vehicleTypePropertyService;
            this.vehicleTypePropertyValueService = vehicleTypePropertyValueService;
        }

        public async Task<IEnumerable<ListingVehicleViewModel>> GetAllVehiclesAsync()
        {
            IEnumerable<ListingVehicleViewModel> allVehicles = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => !v.IsDeleted)
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
                    FuelType = v.FuelType.ToString(),
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
                .Where(v => !v.IsDeleted)
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
                    FuelType = v.FuelType.ToString(),
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
                .Where(v => v.Id == id && !v.IsDeleted)
                .Select(v => new VehicleDetailsViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    OwnerName = v.Owner.UserName,
                    VehicleTypeId = v.VehicleTypeId,
                    VehicleType = v.VehicleType.Name,
                    VehicleTypeCategoryId = v.VehicleTypeCategoryId,
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
        public async Task<bool> SoftDeleteVehicleByIdAsync(Guid id)
        {
            Vehicle vehicleToDelete = await this.vehicleRepository
                .GetByIdAsync(id);

            if (vehicleToDelete == null)
            {
                return false;
            }

            vehicleToDelete.IsDeleted = true;

            await vehicleRepository.SaveChangesAsync();

            return true;
        }
        public async Task<VehicleEditFormViewModel?> GetEditVehicleDetailsByIdAsync(Guid id)
        {
            VehicleEditFormViewModel? editVehicle = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.Id == id && !v.IsDeleted)
                .Select(v => new VehicleEditFormViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    FuelType = v.FuelType,
                    VehicleTypeId = v.VehicleTypeId,
                    VehicleType = v.VehicleType.Name,
                    VehicleTypeCategoryId = v.VehicleTypeCategoryId,
                    VehicleTypeCategory = v.VehicleTypeCategory.Name,
                    Color = v.Color,
                    PricePerDay = v.PricePerDay,
                    DateOfProduction = v.DateOfProduction,
                    CurbWeightInKg = v.CurbWeightInKg,
                    Description = v.Description,
                })
                .FirstOrDefaultAsync();

            if (editVehicle == null)
            {
                return null;
            }

            editVehicle.VehicleTypePropertyValues = await this.vehicleTypePropertyValueService
                .GetVehicleTypePropertyValuesByVehicleIdAsync(id);

            editVehicle.ImageURLs = await this.vehicleImageService
                .GetAllImagesByVehicleIdAsync(id);

            return editVehicle;
        }
        public async Task<bool> CreateVehicle(VehicleCreateFormViewModel viewModel)
        {
            bool hasValidPropertyValueTypes = await this.vehicleTypePropertyService
                .ValidateVehicleTypeProperties(viewModel.VehicleTypeId, viewModel.PropertyValues);

            if (!hasValidPropertyValueTypes)
            {
                return false;
            }

            Vehicle newVehicle = new Vehicle()
            {
                OwnerId = null, // todo: implement owner if a user is putting personal vehicle for rent
                VehicleTypeId = viewModel.VehicleTypeId,
                VehicleTypeCategoryId = viewModel.VehicleTypeCategoryId,
                Make = viewModel.Make,
                Model = viewModel.Model,
                Color = viewModel.Color,
                DateOfProduction = viewModel.DateOfProduction,
                CurbWeightInKg = viewModel.CurbWeightInKg,
                Description = viewModel.Description,
                DateAdded = DateTime.UtcNow,
                PricePerDay = viewModel.PricePerDay,
                FuelType = viewModel.FuelType,
            };

            await vehicleRepository.AddAsync(newVehicle);

            bool successfullyAddedPropertyValues = await this.vehicleTypePropertyValueService
                .AddVehicleTypePropertyValuesAsync(newVehicle.Id, viewModel.PropertyValues);

            if (!successfullyAddedPropertyValues)
            {
                return false;
            }

            bool successfullyAddedVehicleImages = await this.vehicleImageService
                .AddImagesAsync(viewModel.Images, newVehicle.Id);

            if (!successfullyAddedVehicleImages)
            {
                return false;
            }

            await this.vehicleRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateVehicle(VehicleEditFormViewModel viewModel)
        {
            bool hasValidPropertyValueTypes = await this.vehicleTypePropertyService
                .ValidateVehicleTypeProperties(viewModel.VehicleTypeId, viewModel.VehicleTypePropertyInputValues);

            if (!hasValidPropertyValueTypes)
            {
                return false;
            }

            Vehicle vehicleToUpdate = await this.vehicleRepository
                .GetByIdAsync(viewModel.Id);

            if (vehicleToUpdate == null)
            {
                return false;
            }

            vehicleToUpdate.Make = viewModel.Make;
            vehicleToUpdate.Model = viewModel.Model;
            vehicleToUpdate.Color = viewModel.Color;
            vehicleToUpdate.PricePerDay = viewModel.PricePerDay;
            vehicleToUpdate.DateOfProduction = viewModel.DateOfProduction;
            vehicleToUpdate.CurbWeightInKg = viewModel.CurbWeightInKg;
            vehicleToUpdate.Description = viewModel.Description;
            vehicleToUpdate.FuelType = viewModel.FuelType;

            bool successfullyAddedPropertyValues = await this.vehicleTypePropertyValueService
                .UpdateVehicleTypePropertyValuesAsync(vehicleToUpdate.Id, viewModel.VehicleTypePropertyInputValues);

            if (!successfullyAddedPropertyValues)
            {
                return false;
            }

            bool hasNewImages = viewModel.NewImages.Count > 0;

            if (hasNewImages)
            {
                bool successfullyAddedVehicleImages = await this.vehicleImageService
                    .AddImagesAsync(viewModel.NewImages, vehicleToUpdate.Id);

                if (!successfullyAddedVehicleImages)
                {
                    return false;
                }

            }

            await this.vehicleRepository.SaveChangesAsync();

            return true;
        }

    }
}