using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodDonors.Models;
using System.Web.Security;

namespace BloodDonors.Controllers
{
    public class HomeController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Arctic Foundation Decision Support.";

            //pull the recent assessments done by the user

            if (Membership.GetUser() != null)
            {
                Guid thisGUID = new Guid(Membership.GetUser().ProviderUserKey.ToString());

                var UserAssessments = db.Assessments.Where(d => d.UserID == thisGUID).ToList();
                return View(UserAssessments);
            } else
            {
                return View();
            }



        }

        public ActionResult About()
        {
            ViewBag.Message = "app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "contact page.";

            return View();
        }

        public ActionResult DeleteAssessment(int id)
        {

            if (id != 0)
            {
                //find the assessment from the dbcontext

                ModelState.Clear();
                BloodDonors.Models.Assessment thisAssessment = db.Assessments.Find(id);

                if (thisAssessment != null)
                {
                    //remove subobjects first

                    foreach (BloodDonors.Models.Borehole thisBH in thisAssessment.Boreholes)
                    {
                        db.BHSamples.RemoveRange(db.BHSamples.Where(x => x.BoreholdID == thisBH.BoreholeID));
                        db.SaveChanges();                       
                    }

                    db.Boreholes.RemoveRange(db.Boreholes.Where(x => x.AssessmentID == thisAssessment.AssessmentID));

                    foreach (BloodDonors.Models.Transect thisT in thisAssessment.Transects)
                    {
                        db.TransectPoints.RemoveRange(db.TransectPoints.Where(x => x.TransectID == thisT.TransectId));
                        db.SaveChanges();
                        db.TransectImages.RemoveRange(db.TransectImages.Where(x => x.TransectID == thisT.TransectId));
                        db.SaveChanges();
                    }

                    db.Transects.RemoveRange(db.Transects.Where(x => x.AssessmentID == thisAssessment.AssessmentID));

                    db.Assessments.Remove(thisAssessment);
                    db.Entry(thisAssessment).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    return View("Index"); ;
                }
            }

            return View("Index"); ;
        }



    }
}
