using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentDrive.Common.Enums;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;
using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.Vehicle
{
    public class VehicleEditFormViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Make { get; set; } = null!;
        [Required]
        public string Model { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public FuelType FuelType { get; set; }
        [Required]
        public DateTime DateOfProduction { get; set; }
        [Required]
        public double CurbWeightInKg { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int VehicleTypeId { get; set; }
        [Required]
        public int VehicleTypeCategoryId { get; set; }
        [Required]
        public List<VehicleTypePropertyValueInputViewModel> VehicleTypePropertyInputValues { get; set; }
            = new List<VehicleTypePropertyValueInputViewModel>();
        [BindNever]
        public IEnumerable<VehicleTypePropertyValuesViewModel> VehicleTypePropertyValues { get; set; }
            = new List<VehicleTypePropertyValuesViewModel>();
        [BindNever]
        public List<string> ImageURLs { get; set; }
            = new List<string>();
        [BindNever]
        public string? VehicleType { get; set; }
        [BindNever]
        public string? VehicleTypeCategory { get; set; }

        [FromForm]
        public List<IFormFile> NewImages { get; set; }
            = new List<IFormFile>();
    }
}
