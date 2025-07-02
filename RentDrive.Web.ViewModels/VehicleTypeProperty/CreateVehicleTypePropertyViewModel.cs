using RentDrive.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleTypeProperty
{
    public class CreateVehicleTypePropertyViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int VehicleTypeId { get; set; }
        [Required]
        public PropertyValueType ValueType { get; set; }
        [Required]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
