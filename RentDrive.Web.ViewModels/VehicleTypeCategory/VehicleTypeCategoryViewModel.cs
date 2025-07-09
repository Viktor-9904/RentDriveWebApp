namespace RentDrive.Web.ViewModels.VehicleTypeCategory
{
    public class VehicleTypeCategoryViewModel
    {
        public int Id { get; set; }
        public int  VehicleTypeId {  get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
