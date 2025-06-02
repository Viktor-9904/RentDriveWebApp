using RentDrive.Web.ViewModels;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync();
    }
}
