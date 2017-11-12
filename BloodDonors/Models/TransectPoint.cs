using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonors.Models
{
    public class TransectPoint
    {

        //primary key
        [Required]
        [Display(Name = "Id")]
        public int TransectPointID { get; set; }

        [Required]
        [Display(Name = "Point Name")]
        public string PointName { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Elevation")]
        public double Elevation { get; set; }

        [Display(Name = "Station")]
        public double Station { get; set; }

        //foreign key
        public int TransectID { get; set; }

        //public virtual Transect Transect { get; set; }

    }
}