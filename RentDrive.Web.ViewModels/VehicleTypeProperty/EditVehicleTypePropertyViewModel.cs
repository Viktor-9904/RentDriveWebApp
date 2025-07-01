using RentDrive.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleTypeProperty
{
    public class EditVehicleTypePropertyViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public PropertyValueType ValueType { get; set; }
        [Required]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

    }
}
