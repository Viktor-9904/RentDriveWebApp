using RentDrive.Web.ViewModels.VehicleType;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeService
    {
        public Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync();
    }
}
