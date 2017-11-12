using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodDonors.Models
{
    public class Borehole
    {
        //primary key
        public int BoreholeID { get; set; }

        [Required]
        [Display(Name = "Borehole Name")]
        public string BoreholeName { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Elevation")]
        public double Elevation { get; set; }

        [Required]
        [Display(Name = "TransectName")]
        public string TransectName { get; set; }

        [Required]
        [Display(Name = "Station")]
        public double Station { get; set; }

        [Required]
        [Display(Name = "LeftSwathPointX")]
        public double LeftSwathPointX { get; set; }

        [Required]
        [Display(Name = "LeftSwathPointY")]
        public double LeftSwathPointY { get; set; }

        [Required]
        [Display(Name = "RightSwathPointX")]
        public double RightSwathPointX { get; set; }

        [Required]
        [Display(Name = "RightSwathPointY")]
        public double RightSwathPointY { get; set; }

        [Display(Name = "PotentialThawSettlement")]
        public double PotentialThawSettlement { get; set; }

        //foreign key
        public int AssessmentID { get; set; }

        //public virtual Assessment Assessment { get; set; }

        //public virtual Transect Transect { get; set; }

        //navigation property
        public virtual ICollection<BHSample> BHSamples { get; set; }




    }
}