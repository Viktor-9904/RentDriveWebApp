using System.ComponentModel.DataAnnotations;

namespace RentDrive.Data.Models.Enums
{
    public enum UnitOfMeasurement
    {
        [Display(Name = "None")]
        None,

        [Display(Name = "kg")]
        Kg,

        [Display(Name = "km")]
        Km,

        [Display(Name = "M")]
        M,

        [Display(Name = "cm")]
        Cm,

        [Display(Name = "cc")]
        Cc,

        [Display(Name = "L")]
        L,

        [Display(Name = "ml")]
        Ml,

        [Display(Name = "kW")]
        kW,

        [Display(Name = "km/h")]
        KmPerHour,

        [Display(Name = "L/100 km")]
        LPer100Km
    }
}
