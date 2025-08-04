using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using RentDrive.Common.Enums;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Web.ViewModels.Vehicle
{
    public class VehicleCreateFormViewModel
    {
        [Required]
        public string Make { get; set; } = null!;
        [Required]
        public string Model { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public bool IsCompanyProperty { get; set; }
        [Required]
        public FuelType FuelType { get; set; }
        [Required]
        public DateTime DateOfProduction { get; set; }
        [Required]
        public int CurbWeightInKg { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int VehicleTypeId { get; set; }
        [Required]
        public int VehicleTypeCategoryId { get; set; }
        [Required]
        public IEnumerable<VehicleTypePropertyValueInputViewModel> PropertyValues { get; set; }
            = new List<VehicleTypePropertyValueInputViewModel>();
        [Required]
        public List<IFormFile> Images { get; set; } 
            = new List<IFormFile>();
    }
}
