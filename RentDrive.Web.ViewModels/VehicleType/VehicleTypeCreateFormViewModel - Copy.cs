using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.VehicleType
{
    public class VehicleTypeCreateFormViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
