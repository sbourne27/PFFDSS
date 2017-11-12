using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodDonors.Models;


namespace BloodDonor.Controllers
{
    public class JsonDonor
    {
        public int DonorID { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
    }

    public class SearchController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        //
        // AJAX: /Search/SearchByLocation

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult SearchByLocation(float longitude, float latitude)
        {
            var donors = db.Donors.ToList();
            var jsonDonners = from donner in donors
                              select new JsonDonor
                              {
                                  DonorID = donner.DonorID,
                                  Latitude = donner.Latitude,
                                  Longitude = donner.Longitude,
                                  Title = donner.bGroup,
                                  Description = donner.Description,
                              };

            return Json(jsonDonners.ToList());
        }

    }
}