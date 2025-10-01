using RentDrive.Web.ViewModels.Enums;

namespace RentDrive.Web.ViewModels.Vehicle
{
    public class BaseFilterProperties
    {
        public List<PropertyWithCount> Makes { get; set; }
            = new List<PropertyWithCount>();

        public List<PropertyWithCount> Colors { get; set; }
            = new List<PropertyWithCount>();

        public List<FuelTypeEnumViewModel> FuelTypes { get; set; }
            = new List<FuelTypeEnumViewModel>();

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinYearOfProduction { get; set; }
        public int MaxYearOfProduction { get; set; }
    }

    public class PropertyWithCount
    {
        public string Name { get; set; } = null!;
        public int Count { get; set; }
    }
}
