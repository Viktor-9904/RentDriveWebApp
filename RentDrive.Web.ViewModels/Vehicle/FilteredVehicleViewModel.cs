namespace RentDrive.Web.ViewModels.Vehicle
{
    public class FilteredVehicleViewModel
    {
        public int? VehicleTypeId { get; set; }
        public int? VehicleTypeCategoryId { get; set; }
        public List<string> Makes { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>();
        public string? FuelType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public List<FilteredVehicleTypeProperty> Properties { get; set; } = new List<FilteredVehicleTypeProperty>();
    }
}
