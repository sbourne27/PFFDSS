using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BloodDonors.Models
{
    public class BHSample
    {

        //primary key
        [Required]
        [Display(Name = "Id")]
        public int BHSampleId { get; set; }

        [Required]
        [Display(Name = "Depth")]
        public double Depth { get; set; }

        [Required]
        [Display(Name = "Material")]
        public string Material { get; set; }

        [Required]
        [Display(Name = "PF_code")]
        public string PF_code { get; set; }

        [Required]
        [Display(Name = "GMC")]
        public double GMC { get; set; }

        [Required]
        [Display(Name = "Stability")]
        public double Stability { get; set; }

        //foreign key
        public int BoreholdID { get; set; }

        //public virtual Borehole Borehole { get; set; }


    }
}