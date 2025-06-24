using System.ComponentModel.DataAnnotations;

namespace RentDrive.Common.Enums
{
    public enum UnitOfMeasurement
    {
        None,
        Kg,
        Km,
        M,
        cm,
        cc,
        L,
        ml,
        kW,

        [Display(Name = "km/h")]
        KmPerHour,

        [Display(Name = "L/100 km")]
        LPer100Km
    }
}
