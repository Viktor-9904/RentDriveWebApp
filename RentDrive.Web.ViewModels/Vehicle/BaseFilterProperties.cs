using RentDrive.Web.ViewModels.Enums;

namespace RentDrive.Web.ViewModels.Vehicle
{
    public class BaseFilterProperties
    {
        public List<string> Makes { get; set; }
            = new List<string>();

        public List<string> Colors { get; set; }
            = new List<string>();

        public List<FuelTypeEnumViewModel> FuelTypes { get; set; }
            = new List<FuelTypeEnumViewModel>();

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinYearOfProduction { get; set; }
        public int MaxYearOfProduction { get; set; }
    }
}
