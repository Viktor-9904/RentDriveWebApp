namespace RentDrive.Web.ViewModels.Vehicle
{
    public class FilteredVehicleTypeProperty
    {
        public string PropertyId { get; set; } = null!;
        public List<string> Values { get; set; } = new List<string>();
    }
}
