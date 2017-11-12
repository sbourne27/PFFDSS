using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodDonors.Models
{
    public class Transect
    {
        //primary key
        [Required]
        [Display(Name = "Id")]
        public int TransectId { get; set; }

        [Required]
        [Display(Name = "Transect Name")]
        public string TransectName { get; set; }

        //foreign key
        public int AssessmentID { get; set; }

        //public virtual Assessment Assessment { get; set; }

        //navigation properties
        public virtual ICollection<Borehole> Boreholes { get; set; }

        public virtual ICollection<TransectPoint> TransectPoints { get; set; }

        public virtual ICollection<TransectImage> TransectImages { get; set; }


    }
}