using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonors.Models
{
    public class TransectImage
    {
        //primary key
        [Required]
        [Display(Name = "Id")]
        public int TransectImageId { get; set; }

        [Required]
        [Display(Name = "Transect Image Name")]
        public string TransectImageName { get; set; }

        [Required]
        [Display(Name = "Image Alt")]
        public string TransectImageAlt { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }

        [Required]
        [Display(Name = "Start Station")]
        public double StartStation { get; set; }

        [Required]
        [Display(Name = "End Station")]
        public double EndStation { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        //foreign key
        public int TransectID { get; set; }

        //public virtual Transect Transect { get; set; }





    }
}