using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.VehicleType;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeService
    {
        public Task<ServiceResponse<IEnumerable<VehicleTypeViewModel>>> GetAllVehicleTypesAsync();
        public Task<ServiceResponse<bool>> Exists(int vehicleTypeId);
        public Task<ServiceResponse<VehicleTypeCreateFormViewModel?>> CreateNewVehicleType(Guid userId, VehicleTypeCreateFormViewModel viewModel);
        public Task<ServiceResponse<VehicleTypeEditFormViewModel?>> EditVehicleType(Guid userId, VehicleTypeEditFormViewModel viewModel);
        public Task<ServiceResponse<bool>> DeleteVehicleTypeByIdAsync(Guid userId, int id);
    }
}
