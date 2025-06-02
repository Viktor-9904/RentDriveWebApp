using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        public VehicleService(IRepository<Vehicle, Guid> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
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
                    Make = v.Make,
                    Model = v.Model,
                    PricePerHour = v.PricePerHour,
                    //TODO: Fuel Type
                    Description = v.Description,
                })
                .ToArrayAsync();

            return top3RecentVehicles;
        }
    }
}
