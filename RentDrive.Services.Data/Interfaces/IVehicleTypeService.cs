using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeService
    {
        public Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync();
    }
}
