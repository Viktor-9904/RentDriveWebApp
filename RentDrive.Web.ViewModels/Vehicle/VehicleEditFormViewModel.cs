using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RentDrive.Common.Enums;
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
        public FuelType FuelTypeEnum { get; set; }
        [Required]
        public int VehicleTypeId { get; set; }
        [Required]
        public string VehicleType { get; set; } = null!;
        [Required]
        public int VehicleTypeCategoryId { get; set; }
        [Required]
        public string VehicleTypeCategory { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public DateTime DateOfProduction { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public double CurbWeightInKg { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public List<VehicleTypePropertyValuesViewModel> VehicleProperties { get; set; }
            = new List<VehicleTypePropertyValuesViewModel>();
        public List<string> ImageURLs { get; set; }
            = new List<string>();

        public IEnumerable<IFormFile> Images 
            = new List<IFormFile>();
    }
}
