using Microsoft.EntityFrameworkCore;
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
        private readonly IVehicleImageService vehicleImageService;
        public VehicleService(
            IRepository<Vehicle, Guid> vehicleRepository,
            IVehicleImageService vehicleImageService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleImageService = vehicleImageService;
        }

        public async Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync()
        {
            IEnumerable<RecentVehicleIndexViewModel> top3RecentVehicles = await vehicleRepository
                .GetAllAsQueryable()
                .OrderBy(v => v.DateAdded)
                .ThenBy(v => v.Make)
                .ThenBy(v => v.Model)
                .Take(3)
                .Select(v => new RecentVehicleIndexViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    PricePerHour = v.PricePerHour,
                    ImageURL = DefaultImageURL,
                    OwnerName = v.OwnerId.ToString(), //TODO: Change to owner name instead of owner Id.
                    YearOfProduction = v.DateOfProduction.Year,
                    FuelType = v.FuelType.ToString(),
                    Description = v.Description,
                })
                .ToArrayAsync();

            foreach (RecentVehicleIndexViewModel vehicle in top3RecentVehicles)
            {
                string currentVehicleImageURL = await vehicleImageService.GetFirstImageByVehicleIdAsync(vehicle.Id);
                vehicle.ImageURL = currentVehicleImageURL;
            }

            return top3RecentVehicles;
        }
    }
}
