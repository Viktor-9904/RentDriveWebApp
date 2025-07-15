using RentDrive.Web.ViewModels.VehicleType;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeService
    {
        public Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync();
        public Task<bool> Exists(int vehicleTypeId);
        public Task<bool> DeleteVehicleTypeByIdAsync(int id);
    }
}
