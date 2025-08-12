namespace RentDrive.Web.ViewModels.VehicleTypePropertyValue
{
    public class FilterProperty
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = null!;
        public IEnumerable<FilterPropertyValue> PropertyValues { get; set; } 
            = new List<FilterPropertyValue>();
    }
}
