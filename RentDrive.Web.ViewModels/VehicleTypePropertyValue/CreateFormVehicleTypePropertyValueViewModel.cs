using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleTypePropertyValue
{
    public class CreateFormVehicleTypePropertyValueViewModel
    {
        [Required]
        public Guid PropertyId { get; set; }
        [Required]
        public string Value { get; set; } = null!;
    }
}
