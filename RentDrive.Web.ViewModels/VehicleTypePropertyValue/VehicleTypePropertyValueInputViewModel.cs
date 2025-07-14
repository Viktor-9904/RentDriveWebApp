using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleTypePropertyValue
{
    public class VehicleTypePropertyValueInputViewModel
    {
        [Required]
        public Guid PropertyId { get; set; }
        [Required] 
        public string Value { get; set; } = null!;
    }
}
