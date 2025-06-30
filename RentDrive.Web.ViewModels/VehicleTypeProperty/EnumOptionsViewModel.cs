namespace RentDrive.Web.ViewModels.VehicleTypeProperty
{
    public class EnumOptionsViewModel
    {
        public IEnumerable<ValueTypeViewModel> ValueTypes { get; set; }
            = new List<ValueTypeViewModel>();

        public IEnumerable<UnitOfMeasurementViewModel> UnitOfMeasurements { get; set; }
            = new List<UnitOfMeasurementViewModel>();
    }
}
