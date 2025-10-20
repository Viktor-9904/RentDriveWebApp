using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Enums;
using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleReview;

using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.Company;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.Vehicle;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Services.Data
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        private readonly IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository;
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;

        private readonly IBaseService baseService;
        private readonly IVehicleImageService vehicleImageService;
        private readonly IVehicleTypePropertyService vehicleTypePropertyService;
        private readonly IVehicleTypePropertyValueService vehicleTypePropertyValueService;
        public VehicleService(
            IRepository<Vehicle, Guid> vehicleRepository,
            IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository,
            IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository,
            IRepository<ApplicationUser, Guid> applicationUserRepository,
            IBaseService baseService,
            IVehicleImageService vehicleImageService,
            IVehicleTypePropertyService vehicleTypePropertyService,
            IVehicleTypePropertyValueService vehicleTypePropertyValueService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
            this.vehicleTypePropertyValueRepository = vehicleTypePropertyValueRepository;
            this.applicationUserRepository = applicationUserRepository;
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
                .Include(v => v.Reviews)
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
                    OwnerName = v.Owner.UserName,
                    StarsRating = v.Reviews.Select(vr => (double?)vr.Stars).Average() ?? 0,
                    ReviewCount = v.Reviews.Count()
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
                    StarsRating = v.Reviews.Select(vr => (double?)vr.Stars).Average() ?? 0,
                    ReviewCount = v.Reviews.Count()
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
                .Include(v => v.Owner)
                .Include(v => v.VehicleImages)
                .Include(v => v.Reviews)
                .ThenInclude(vr => vr.Reviewer)
                .Where(v => v.Id == id && !v.IsDeleted)
                .Select(v => new VehicleDetailsViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    IsCompanyProperty = v.OwnerId == new Guid(CompanyId),
                    OwnerId = v.OwnerId ?? Guid.Empty,
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
                    FuelType = v.FuelType.ToString(),
                    Description = v.Description,
                    ImageURLS = v.VehicleImages.Select(vi => vi.ImageURL).ToList(),
                    StarsRating = v.Reviews.Select(vr => (double?)vr.Stars).Average() ?? 0,
                    ReviewCount = v.Reviews.Count(),
                    VehicleReviews = v.Reviews.Select(vr => new VehicleReviewListItemViewModel()
                    {
                        Username = vr.Reviewer.UserName!,
                        Comment = vr.Comment,
                        StarRating = vr.Stars,
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync();

            if (vehicleDetails == null)
            {
                return null;
            }

            vehicleDetails.VehicleProperties = await this.vehicleTypePropertyValueService
                .GetVehicleTypePropertyValuesByVehicleIdAsync(id);

            return vehicleDetails;
        }

        public async Task<bool> SoftDeleteVehicleByIdAsync(Guid deletedByUserId, Guid vehicleId)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(deletedByUserId);

            if (currentUser == null)
            {
                return false; // user not found
            }

            Vehicle? vehicleToDelete = await this.vehicleRepository
                .GetAllAsQueryable()
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicleToDelete == null)
            {
                return false; // vehicle not found
            }

            UserType currentUserType = currentUser.UserType;

            bool canDelete =
                currentUserType == UserType.CompanyEmployee ||
                vehicleToDelete.OwnerId == currentUser.Id;

            if (!canDelete)
            {
                return false; // unauthorized user
            }

            vehicleToDelete.IsDeleted = true;

            await vehicleRepository.SaveChangesAsync();

            return true;
        }

        public async Task<VehicleEditFormViewModel?> GetEditVehicleDetailsByIdAsync(Guid editorId, Guid vehicleId)
        {
            Vehicle? vehicleEntity = await this.vehicleRepository
                .GetAllAsQueryable()
                .Include(v => v.Owner)
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleTypeCategory)
                .FirstOrDefaultAsync(v => v.Id == vehicleId && !v.IsDeleted);

            if (vehicleEntity == null || vehicleEntity.Owner == null)
            {
                return null;
            }

            ApplicationUser? editor = await this.applicationUserRepository
                .GetByIdAsync(editorId);

            if (editor == null)
            {
                return null; // editor user not found;
            }


            bool vehicleIsCompanyOwned = vehicleEntity.OwnerId == new Guid(CompanyId);
            UserType currentUserType = editor.UserType;

            bool canEdit =
                (vehicleIsCompanyOwned && currentUserType == UserType.CompanyEmployee) ||
                (!vehicleIsCompanyOwned && vehicleEntity.OwnerId == editorId);

            if (!canEdit)
            {
                return null; // unauthorized user
            }

            VehicleEditFormViewModel? editVehicle = new VehicleEditFormViewModel()
            {
                Id = vehicleEntity.Id,
                Make = vehicleEntity.Make,
                Model = vehicleEntity.Model,
                FuelType = vehicleEntity.FuelType,
                VehicleTypeId = vehicleEntity.VehicleTypeId,
                VehicleType = vehicleEntity.VehicleType.Name,
                VehicleTypeCategoryId = vehicleEntity.VehicleTypeCategoryId,
                VehicleTypeCategory = vehicleEntity.VehicleTypeCategory.Name,
                Color = vehicleEntity.Color,
                PricePerDay = vehicleEntity.PricePerDay,
                DateOfProduction = vehicleEntity.DateOfProduction,
                CurbWeightInKg = vehicleEntity.CurbWeightInKg,
                Description = vehicleEntity.Description,
            };

            if (editVehicle == null)
            {
                return null;
            }

            editVehicle.VehicleTypePropertyValues = await this.vehicleTypePropertyValueService
                .GetVehicleTypePropertyValuesByVehicleIdAsync(vehicleId);

            editVehicle.ImageURLs = await this.vehicleImageService
                .GetAllImagesByVehicleIdAsync(vehicleId);

            return editVehicle;
        }

        public async Task<bool> CreateVehicle(string userdId, VehicleCreateFormViewModel viewModel)
        {
            ApplicationUser? user = await this.applicationUserRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(au => au.Id.ToString() == userdId);

            if (user == null)
            {
                return false;
            }

            bool hasValidPropertyValueTypes = await this.vehicleTypePropertyService
                .ValidateVehicleTypeProperties(viewModel.VehicleTypeId, viewModel.PropertyValues);

            if (!hasValidPropertyValueTypes)
            {
                return false;
            }

            Vehicle newVehicle = new Vehicle()
            {
                OwnerId = viewModel.IsCompanyProperty && Guid.TryParse(CompanyId, out Guid parsedCompanyId) ? parsedCompanyId : user.Id,
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

        public async Task<bool> UpdateVehicle(Guid editorId, VehicleEditFormViewModel viewModel)
        {
            Vehicle? vehicleToUpdate = await this.vehicleRepository
                .GetAllAsQueryable()
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == viewModel.Id);

            if (vehicleToUpdate == null)
            {
                return false; // vehicle not found
            }


            ApplicationUser? editor = await this.applicationUserRepository
                .GetByIdAsync(editorId);

            if (editor == null)
            {
                return false; // editor user not found;
            }

            bool vehicleIsCompanyOwned = vehicleToUpdate.OwnerId == new Guid(CompanyId);
            UserType currentUserType = editor.UserType;

            bool canEdit =
                (vehicleIsCompanyOwned && currentUserType == UserType.CompanyEmployee) ||
                (!vehicleIsCompanyOwned && vehicleToUpdate.OwnerId == editorId);

            if (!canEdit)
            {
                return false; // unauthorized user
            }

            bool hasValidPropertyValueTypes = await this.vehicleTypePropertyService
                .ValidateVehicleTypeProperties(viewModel.VehicleTypeId, viewModel.VehicleTypePropertyInputValues);

            if (!hasValidPropertyValueTypes)
            {
                return false; // edited vehicle doesn't have valid property types
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

        public async Task<decimal> GetVehiclePricePerDayByVehicleId(Guid id)
        {
            Vehicle vehicle = await this.vehicleRepository
                .GetByIdAsync(id);

            return vehicle.PricePerDay;
        }

        public async Task<int> GetUserListedVehicleCountAsync(Guid userId)
        {
            int ownedVehicleCount = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.OwnerId == userId)
                .CountAsync();

            return ownedVehicleCount;
        }

        public async Task<IEnumerable<UserVehicleViewModel>> GetUserVehiclesByIdAsync(string userId)
        {
            IEnumerable<UserVehicleViewModel> userVehicles = await this.vehicleRepository
                .GetAllAsQueryable()
                .Include(v => v.VehicleImages)
                .Include(v => v.Rentals)
                .Where(v =>
                    v.OwnerId.ToString() == userId &&
                    v.IsDeleted == false)
                .Select(v => new UserVehicleViewModel()
                {
                    Id = v.Id,
                    ImageUrl = v.VehicleImages.FirstOrDefault().ImageURL ?? "images/default-vehicle.jpg",
                    Make = v.Make,
                    Model = v.Model,
                    FuelType = v.FuelType.ToString(),
                    PricePerDay = v.PricePerDay,
                    TimesBooked = v.Rentals.Count(),
                    StarRating = v.Reviews.Select(vr => (double?)vr.Stars).Average() ?? 0,
                    ReviewCount = v.Reviews.Count()
                })
                .ToListAsync();

            return userVehicles;
        }

        public async Task<BaseFilterProperties> GetBaseFilterPropertiesAsync(int? vehicleTypeId = null, int? vehicleTypeCategoryId = null)
        {
            IQueryable<Vehicle> vehiclesQuery = this.vehicleRepository.GetAllAsQueryable().Where(v => v.IsDeleted == false);

            if (vehicleTypeId.HasValue && vehicleTypeId.Value > 0)
            {
                vehiclesQuery = vehiclesQuery.Where(v => v.VehicleTypeId == vehicleTypeId.Value);
            }

            if (vehicleTypeCategoryId.HasValue && vehicleTypeCategoryId.Value > 0)
            {
                vehiclesQuery = vehiclesQuery.Where(v => v.VehicleTypeCategoryId == vehicleTypeCategoryId.Value);
            }

            if (!await vehiclesQuery.AnyAsync())
            {
                return new BaseFilterProperties
                {
                    Makes = new List<PropertyWithCount>(),
                    Colors = new List<PropertyWithCount>(),
                    FuelTypes = new List<FuelTypeEnumViewModel>(),
                    MinPrice = 0,
                    MaxPrice = 0,
                    MinYearOfProduction = 0,
                    MaxYearOfProduction = 0
                };
            }

            List<PropertyWithCount> makesWithCounts = await vehiclesQuery
                .GroupBy(v => v.Make)
                .Select(g => new PropertyWithCount
                {
                    Name = g.Key,
                    Count = g.Count()
                })
                .OrderBy(m => m.Name)
                .ToListAsync();

            List<PropertyWithCount> colorsWithCounts = await vehiclesQuery
                .GroupBy(v => v.Color)
                .Select(g => new PropertyWithCount
                {
                    Name = g.Key,
                    Count = g.Count()
                })
                .OrderBy(m => m.Name)
                .ToListAsync();

            List<FuelTypeEnumViewModel> fuelTypes = await vehiclesQuery
                .Select(v => new FuelTypeEnumViewModel
                {
                    Id = (int)v.FuelType,
                    Name = v.FuelType.ToString()
                })
                .Distinct()
                .OrderBy(f => f.Name)
                .ToListAsync();

            decimal minPrice = await vehiclesQuery.MinAsync(v => v.PricePerDay);
            decimal maxPrice = await vehiclesQuery.MaxAsync(v => v.PricePerDay);

            int minYear = await vehiclesQuery.MinAsync(v => v.DateOfProduction.Year);
            int maxYear = await vehiclesQuery.MaxAsync(v => v.DateOfProduction.Year);

            BaseFilterProperties baseProperties = new BaseFilterProperties()
            {
                Makes = makesWithCounts,
                Colors = colorsWithCounts,
                FuelTypes = fuelTypes,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinYearOfProduction = minYear,
                MaxYearOfProduction = maxYear,
            };

            return baseProperties;
        }

        public async Task<IEnumerable<Guid>> GetFilteredVehicles(FilteredVehiclesViewModel filter)
        {
            IQueryable<Vehicle> query = this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.IsDeleted == false)
                .Include(v => v.VehicleTypePropertyValues);

            if (filter.VehicleTypeId.HasValue)
            {
                query = query.Where(v => v.VehicleTypeId == filter.VehicleTypeId.Value);
            }

            if (filter.VehicleTypeCategoryId.HasValue)
            {
                query = query.Where(v => v.VehicleTypeCategoryId == filter.VehicleTypeCategoryId.Value);
            }

            if (filter.Makes != null && filter.Makes.Any())
            {
                query = query.Where(v => filter.Makes.Contains(v.Make));
            }

            if (filter.Colors != null && filter.Colors.Any())
            {
                query = query.Where(v => filter.Colors.Contains(v.Color));
            }

            if (!string.IsNullOrEmpty(filter.FuelType))
            {
                query = query.Where(v => v.FuelType.ToString() == filter.FuelType);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(v => v.PricePerDay >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(v => v.PricePerDay <= filter.MaxPrice.Value);
            }

            if (filter.MinYear.HasValue)
            {
                query = query.Where(v => v.DateOfProduction.Year >= filter.MinYear.Value);
            }

            if (filter.MaxYear.HasValue)
            {
                query = query.Where(v => v.DateOfProduction.Year <= filter.MaxYear.Value);
            }

            if (filter.Properties != null && filter.Properties.Any())
            {
                foreach (FilteredVehicleTypeProperty property in filter.Properties)
                {
                    if (property.Values != null && property.Values.Any())
                    {
                        string propertyId = property.PropertyId;
                        List<string> values = property.Values;

                        query = query.Where(v => v.VehicleTypePropertyValues
                            .Any(vtpv => vtpv.VehicleTypePropertyId.ToString() == propertyId && values.Contains(vtpv.PropertyValue)));
                    }
                }
            }

            IEnumerable<Guid> filteredVehicles = await query
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleTypeCategory)
                .OrderBy(lvvm => lvvm.Make)
                .ThenBy(lvvm => lvvm.Model)
                .Where(v =>
                    EF.Functions.ILike(v.Make, $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.Model, $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.VehicleType.Name, $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.VehicleTypeCategory.Name, $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.FuelType.ToString(), $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.DateOfProduction.Year.ToString(), $"%{filter.SearchQuery}%") ||
                    EF.Functions.ILike(v.Description, $"%{filter.SearchQuery}%"))
                .Select(v => v.Id)
                .ToListAsync();

            return filteredVehicles;
        }

        public async Task<IEnumerable<ListingVehicleViewModel>> GetSearchQueryVehicles(string searchQuery)
        {
            IEnumerable<ListingVehicleViewModel> result = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.IsDeleted == false)
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleTypeCategory)
                .Include(v => v.VehicleImages)
                .Where(v =>
                    EF.Functions.ILike(v.Make, $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.Model, $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.VehicleType.Name, $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.VehicleTypeCategory.Name, $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.FuelType.ToString(), $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.DateOfProduction.Year.ToString(), $"%{searchQuery}%") ||
                    EF.Functions.ILike(v.Description, $"%{searchQuery}%"))
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
                     ImageURL = v.VehicleImages.Select(vi => vi.ImageURL).FirstOrDefault() ?? DefaultImageURL,
                     OwnerName = v.Owner.UserName,
                     StarsRating = v.Reviews.Select(vr => (double?)vr.Stars).Average() ?? 0,
                     ReviewCount = v.Reviews.Count()
                 })
                .OrderBy(lvvm => lvvm.Make)
                .ThenBy(lvvm => lvvm.Model)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<string>> GetAllVehicleMakesAsync()
        {
            IEnumerable<string> makes = await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.IsDeleted == false)
                .Select(v => v.Make)
                .ToListAsync();

            return makes;
        }

        public async Task<int> GetActiveListings()
        {
            return await this.vehicleRepository
                .GetAllAsQueryable()
                .Where(v => v.IsDeleted == false)
                .CountAsync();
        }
    }
}