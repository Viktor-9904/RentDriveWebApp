using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleTypeCategory
{
    public class VehicleTypeCategoryCreateFormViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int VehicleTypeId { get; set; }
    }
}
