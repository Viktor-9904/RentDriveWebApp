﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RentDrive.Web.ViewModels.Rental
{
    public class RentVehicleViewModel
    {
        [Required]
        public Guid RenterId { get; set; }
        [Required]
        public IEnumerable<DateTime> BookedDates { get; set; }
            = new List<DateTime>();
    }
}
