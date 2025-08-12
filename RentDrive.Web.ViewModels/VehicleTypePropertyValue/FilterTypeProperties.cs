namespace RentDrive.Web.ViewModels.VehicleTypePropertyValue
{
    public class FilterTypeProperties
    {
        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<FilterProperty> Properties { get; set; } 
            = new List<FilterProperty>();
    }
}
