using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodDonors.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Hosting;
using System.Net;
using System.Net.Http;
using System.Json;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace BloodDonors.Controllers
{
    public class WizardController : Controller
    {

        private DonorDBContext db = new DonorDBContext();
        

        public int CurrentAssessmentID = 0;

        //
        // GET: /Wizard/
        [HttpGet]
        [Authorize]
        public ActionResult Open(int id)
        {


            

            string theToken = "";

            try
            {

                string ClientID = "elBvfMc5CdkWaMJv";
                string ClientSecret = "a4b4b1bcbfc544e5934f235093975f10";

                AGOL thisAgol = new AGOL(ClientID, ClientSecret);

                theToken = thisAgol.GetToken(ClientID, ClientSecret);

            }
            catch (Exception e)
            {

            }

            ViewBag.EsriToken = theToken;


            if (id != 0)
            {
                //find the assessment from the dbcontext


                ModelState.Clear();
                BloodDonors.Models.Assessment thisAssessment = db.Assessments.Find(id);

                if (thisAssessment != null)
                {

                    EvaluateStability(thisAssessment);
                    return View("Index", thisAssessment);

                } else
                {
                    return null;
                }

            } else {

                //check if logged in.  If not, then force login



                BloodDonors.Models.Assessment thisAssessment = new BloodDonors.Models.Assessment();

                thisAssessment.UserID = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                thisAssessment.CreatedDate = DateTime.Now;
                thisAssessment.LastModifiedDate = DateTime.Now;


                thisAssessment.AssessmentID = 1;
                thisAssessment.AssessmentName = "New Assessment";
                thisAssessment.StructureType = Models.StructureType.Light;
                thisAssessment.LightStructureUse = BloodDonors.Models.LightStructureUse.Offices;
                thisAssessment.HeavyStructureUse = BloodDonors.Models.HeavyStructureUse.Hospital;
                thisAssessment.LinearStructureUse = BloodDonors.Models.LinearStructureUse.Road;
                thisAssessment.CurrentStep = "WS0";
                thisAssessment.FoundationElevation = BloodDonors.Models.FoundationElevation.Grade;
                thisAssessment.LifeSafetyRequirement = BloodDonors.Models.LifeSafetyRequirement.Med;
                thisAssessment.AcceptableFoundationFailureRiskLevel = BloodDonors.Models.AcceptableFoundationFailureRiskLevel.Low;
                thisAssessment.StructureHeated = BloodDonors.Models.StructureHeated.Heated;
                thisAssessment.ObservedIceWedges = BloodDonors.Models.ObservedIceWedges.NoIceWedges;
                thisAssessment.TopographyEstimate = BloodDonors.Models.TopographyEstimate.Moderate;
                thisAssessment.ThermoKarstPresent = Models.ThermoKarstPresent.NoThermoKarstPresent;
                thisAssessment.VegetationType = Models.VegetationType.Deciduous;
                thisAssessment.BlackSprucePresent = BloodDonors.Models.BlackSprucePresent.BlackSpruceNotPresent;
                thisAssessment.DrunkenForestPresent = BloodDonors.Models.DrunkenForestPresent.DrunkenForestNotPresent;
                thisAssessment.StreamsPresent = BloodDonors.Models.StreamsPresent.StreamsNotPresent;
                thisAssessment.MarshesStandingWaterPresent = BloodDonors.Models.MarshesStandingWaterPresent.MarshesStandingWaterPresentNotPresent;
                thisAssessment.ExistingStructureType = Models.ExistingStructureType.Light;
                thisAssessment.ExistingStructuresHeated = BloodDonors.Models.ExistingStructuresHeated.ExistingStructuresNotHeated;
                thisAssessment.ExistingStructuresFoundationCondition = BloodDonors.Models.ExistingStructuresFoundationCondition.Good;
                thisAssessment.SiteLatitude = 62.0;
                thisAssessment.SiteLongitude = -100.0;
                thisAssessment.AssessmentArea = 0.0;
                thisAssessment.StructureLength = 50.0;
                thisAssessment.StructureWidth = 50.0;
                thisAssessment.LifeSpan = 30;
                thisAssessment.Stories = 1;
                thisAssessment.MT = 0;
                thisAssessment.IC = 0;
                thisAssessment.V = 0;
                thisAssessment.R = 0;
                thisAssessment.C = 0;
                thisAssessment.DDF = 0;
                thisAssessment.ShapeCoordinates = "0,0";
                thisAssessment.Boreholes = new List<Models.Borehole>();

                if (ModelState.IsValid)
                {

                    try
                    {

                        db.Configuration.AutoDetectChangesEnabled = true;

                        db.Assessments.Add(thisAssessment);
                        this.CurrentAssessmentID = thisAssessment.AssessmentID;
                        db.SaveChanges();

                        this.CurrentAssessmentID = thisAssessment.AssessmentID;
                        // return RedirectToAction("Index");

                        Console.Write(thisAssessment.AssessmentID);
                    }

                    catch (DbEntityValidationException dbEx)

                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                                        validationError.PropertyName,
                                                        validationError.ErrorMessage);
                            }
                        }
                    }




                }




                return View("Index", thisAssessment);


            }
            
        }

        private void EvaluateStability(BloodDonors.Models.Assessment theAssessment)
        {

            //address any zero GMCs at the surface of the borehole due to first sample being below surface.
            foreach (BloodDonors.Models.Borehole searchBH in theAssessment.Boreholes)
            {
                int BHIndex = 0;
                int FirstValidIndex = 0;
                double FirstGMC = 0;
                foreach (BloodDonors.Models.BHSample searchBHS in searchBH.BHSamples)
                {
                    BHIndex = BHIndex + 1;
                    if (searchBHS.GMC > 0)
                    {
                        FirstGMC = searchBHS.GMC;
                        break;
                    }
                }

                FirstValidIndex = BHIndex;
                BHIndex = 0;
                foreach (BloodDonors.Models.BHSample searchBHS in searchBH.BHSamples)
                {
                    BHIndex = BHIndex + 1;
                    searchBHS.GMC = FirstGMC;
                    if (BHIndex >= FirstValidIndex)
                    {
                        break;
                    }
                }



            }

            //evaluate the stability of each sample
            //get the lookup table
            var PSIcolumn1 = new List<string>();
            var PSIcolumn2 = new List<string>();
            var PSIcolumn3 = new List<string>();
            var PSIcolumn4 = new List<string>();

            using (StreamReader rd = new StreamReader(Server.MapPath("~/Data/PSITable.csv")))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(',');
                    PSIcolumn1.Add(splits[0]); //Frozen
                    PSIcolumn2.Add(splits[1]); //USCS_code
                    PSIcolumn3.Add(splits[2]); //GMC
                    PSIcolumn4.Add(splits[3]); //PSI
                }
            }

            foreach (Models.Borehole loopBH in theAssessment.Boreholes)
            {
                foreach (Models.BHSample loopBHS in loopBH.BHSamples)
                {
                    for (int i = 1; i < PSIcolumn1.Count - 1; i++)
                    {
                        if (loopBHS.Material == PSIcolumn2[i])
                        {
                            if (PSIcolumn1[i] == loopBHS.PF_code)
                            {
                                double thisGMC = Convert.ToDouble(loopBHS.GMC);
                                double lookupGMC = Convert.ToDouble(PSIcolumn3[i]);

                                if (thisGMC < 10)
                                {
                                    loopBHS.Stability = 5;
                                    break;
                                } else
                                {
                                    if (lookupGMC < thisGMC)
                                    {
                                        loopBHS.Stability = Convert.ToDouble(PSIcolumn4[i]);
                                    }
                                    else {
                                        break;
                                    }
                                }

                               
                            }


                        }
                    }
                }
            }



        }


        //call for getting the token for the ArcGIS App that holds the GIS data
        [HttpPost]
        public ActionResult GetEsriToken ()
        {

            try
            {

                string ClientID = "elBvfMc5CdkWaMJv";
                string ClientSecret = "a4b4b1bcbfc544e5934f235093975f10";

                AGOL thisAgol = new AGOL(ClientID, ClientSecret);

                string theToken = thisAgol.GetToken(ClientID, ClientSecret);

                return null;
                                
            }
            catch (Exception e)
            {
                // catch exception and log it
                return null;
            }
        }

        // POST: /Wizard/
        [HttpPost]
        public ActionResult Upload(string theTransectName, string StartStation, string EndStation)
        {

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);

                    //get the image data
                    //byte[] imageData = null;
                    ////using (var binaryReader = new BinaryReader(file.InputStream))
                    ////{
                    ////    imageData = binaryReader.ReadBytes(file.ContentLength);
                    ////}

                    Transect TargetTransect = null;
                    foreach (Transect thisT in db.Transects)
                    {
                        if (thisT.TransectName.Trim() == theTransectName.Trim())
                        {
                            TargetTransect = thisT;
                            break;
                        }
                    }

                    if (TargetTransect != null)
                    {
                        //save new record in database
                        TransectImage newRecord = new TransectImage();
                        newRecord.TransectImageName = ImageName;
                        newRecord.TransectImageAlt = ImageName;
                        newRecord.ImagePath = "~/Images/" + ImageName;
                        newRecord.ContentType = "ResistivityImage";
                        newRecord.StartStation = Convert.ToDouble(StartStation);
                        newRecord.EndStation = Convert.ToDouble(EndStation);
                        newRecord.Type = "ResistivityImage";
                        newRecord.TransectID = Convert.ToInt32(TargetTransect.TransectId);
                        db.TransectImages.Add(newRecord);
                        db.SaveChanges();
                    }


                }

            }

            ////Display records
            return null;
        }



        // POST: /Wizard/
        [HttpPost]
        public ActionResult Update(BloodDonors.Models.Assessment theAssessment, string theTransectBlob, string theTransectProps, string BHLocationData, string BHSampleData, string CurrentTransectName)
        {

            //ModelState.Clear();

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //BloodDonors.Models.Assessment theAssessment = js.Deserialize<BloodDonors.Models.Assessment>(theAssessmentString);


            var errors = ModelState.Values.SelectMany(v => v.Errors);

            

            if (ModelState.IsValid)

                try
                {

                    // db.Assessments.Add(theAssment);

                    BloodDonors.Models.Assessment DBAssessment =  db.Assessments.Find(theAssessment.AssessmentID);

                    Boolean BHChanged = false;
                    Boolean TransectChanged = false;

                    if (DBAssessment != null)
                    {
                        Type typeView = theAssessment.GetType();
                        System.Reflection.PropertyInfo[] propertiesView = typeView.GetProperties();

                        Type typeModel = DBAssessment.GetType();
                        System.Reflection.PropertyInfo[] propertiesModel = typeView.GetProperties();

                        //DBAssessment.AssessmentName = theAssessment.AssessmentName;
                        //DBAssessment.AcceptableFoundationFailureRiskLevel = theAssessment.AcceptableFoundationFailureRiskLevel;
                        //DBAssessment.AssessmentArea = theAssessment.AssessmentArea;

                        foreach (System.Reflection.PropertyInfo propertyView in propertiesView)
                        {
                            foreach (System.Reflection.PropertyInfo PropertyModel in propertiesModel)
                            {

                                if (PropertyModel.Name == propertyView.Name)
                                {
                                    string thisPropViewValue = Convert.ToString(propertyView.GetValue(theAssessment, null));
                                    string thisPropDBValue = Convert.ToString(propertyView.GetValue(DBAssessment, null));

                                    if (thisPropViewValue != "")
                                    {
                                        if (thisPropDBValue != thisPropViewValue)
                                        {
                                            PropertyModel.SetValue(DBAssessment, propertyView.GetValue(theAssessment, null), null);
                                            db.Entry(DBAssessment).State = System.Data.Entity.EntityState.Modified;
                                        }
                                    }


                                }

                            }
                        }

                        //transects
                        if (theTransectBlob != null && theTransectBlob != "")
                        {

                            if (CurrentTransectName != null & CurrentTransectName != "")
                            {

                                //reconcile the transectblob with the model
                                //delete transects that are in the model and not in the blob
                                //add transects to the model that are in the blob but not in the model

                                var TrasectProps = theTransectProps.Split(';');
                                var tPoints = theTransectBlob.Split(';');

                                for (int i =0; i < TrasectProps.Length; i++)
                                {
                                    var TP1 = TrasectProps[i].Split(',');
                                    if (TP1.Length > 1)
                                    {
                                        var TPoints1 = tPoints[i].Split(',');
                                        string theTID = TP1[0].Trim();
                                        string theLength = TP1[1].Trim();

                                        //find if the transect is already there

                                        Boolean TThere = false;
                                        foreach (BloodDonors.Models.Transect thisTransect in DBAssessment.Transects)
                                        {
                                            if (thisTransect.TransectName == theTID)
                                            {
                                                TThere = true;
                                                break;
                                            }
                                        }
                                        if (TThere == false)
                                        {
                                            BloodDonors.Models.Transect NewTransect = new BloodDonors.Models.Transect();
                                            NewTransect.TransectName = theTID;
                                            NewTransect.AssessmentID = DBAssessment.AssessmentID;
                                            NewTransect.TransectPoints = new List<Models.TransectPoint>();
                                            NewTransect.Boreholes = new List<Models.Borehole>();

                                            DBAssessment.Transects.Add(NewTransect);
                                            db.Entry(DBAssessment).State = System.Data.Entity.EntityState.Modified;
                                            db.Entry(NewTransect).State = System.Data.Entity.EntityState.Added;

                                            TransectChanged = true;

                                            //also add the points
                                            var XY = TPoints1;
                                            for (int k = 1; k < XY.Length; k++)
                                            {
                                                BloodDonors.Models.TransectPoint NewPoint = new BloodDonors.Models.TransectPoint();

                                                NewPoint.Longitude = Convert.ToDouble(XY[k - 1]);
                                                NewPoint.Latitude = Convert.ToDouble(XY[k]);
                                                NewPoint.PointName = Convert.ToString(k);

                                                NewTransect.TransectPoints.Add(NewPoint);
                                                //db.Entry(NewTransect).State = System.Data.Entity.EntityState.Modified;
                                                db.Entry(NewPoint).State = System.Data.Entity.EntityState.Added;
                                                k = k + 1;
                                            }
                                            //db.Transects.Add(NewTransect);
                                        }
                                    }
                                }

                                Boolean TransectDeleted = false;
                                do
                                {
                                    TransectDeleted = false;

                                    foreach (BloodDonors.Models.Transect thisTransect in DBAssessment.Transects)
                                    {

                                        for (int i = 0; i < TrasectProps.Length; i++)
                                        {
                                            if (TrasectProps[i] != "")
                                            {
                                                var TP1 = TrasectProps[i].Split(',');
                                                var TPoints1 = tPoints[i].Split(',');
                                                string theTID = TP1[0].Trim();
                                                string theLength = TP1[1].Trim();

                                                //find if the transect is already there

                                                Boolean TinBlob = false;
                                                if (thisTransect.TransectName == theTID)
                                                {
                                                    TinBlob = true;
                                                }
                                                if (TinBlob == false)
                                                {
                                                    //delete the transect from the database
                                                    DBAssessment.Transects.Remove(thisTransect);
                                                    db.Entry(DBAssessment).State = System.Data.Entity.EntityState.Modified;
                                                    db.Entry(thisTransect).State = System.Data.Entity.EntityState.Deleted;
                                                    TransectDeleted = true;
                                                    break;
                                            
                                                }
                                            }
                                        }

                                        if (TransectDeleted)
                                        {
                                            break;
                                        }
                                    }

                                } while (TransectDeleted == true);

                                


                            }

                         


                        }

                        //delete all transects if the blob and props are empty
                        if (theTransectBlob == "" && theTransectProps == "")
                        {
                            Boolean TransectDeleted = false;
                            do
                            {
                                TransectDeleted = false;

                                foreach (BloodDonors.Models.Transect thisTransect in DBAssessment.Transects)
                                {
                                    DBAssessment.Transects.Remove(thisTransect);
                                    db.Entry(DBAssessment).State = System.Data.Entity.EntityState.Modified;
                                    db.Entry(thisTransect).State = System.Data.Entity.EntityState.Deleted;
                                    TransectDeleted = true;
                                    break;

                                }
                                                        
                            } while (TransectDeleted == true);

                        }




                        //update boreholes
                        if (BHLocationData != null && BHLocationData != "")
                        {

                            var BHLocations = BHLocationData.Split(';');
                            var BHSamples = BHSampleData.Split(';');

                            for (int i = 0; i < BHLocations.Length-1; i++)
                            {
                                var BH1 = BHLocations[i].Split(',');
                                var BHSamples1 = BHSamples[i].Split(',');

                                string theBHName = BH1[0].Trim();

                                if (theBHName.Length > 0)
                                {
                                    string theLongitude = BH1[2].Trim();
                                    string theLatitude = BH1[1].Trim();
                                    string theStation = BH1[3].Trim();
                                    string theElevation = BH1[4].Trim();



                                    //find if the BH is already there
                                    Boolean BHThere = false;
                                    foreach (BloodDonors.Models.Borehole thisBH in DBAssessment.Boreholes)
                                    {
                                        if (thisBH.BoreholeName == theBHName)
                                        {
                                            BHThere = true;
                                            break;
                                        }
                                    }
                                    if (BHThere == false)
                                    {
                                        BloodDonors.Models.Borehole NewBorehole = new BloodDonors.Models.Borehole();
                                        NewBorehole.BoreholeName = theBHName;
                                        NewBorehole.AssessmentID = DBAssessment.AssessmentID;
                                        NewBorehole.Latitude = Convert.ToDouble(theLatitude);
                                        NewBorehole.Longitude = Convert.ToDouble(theLongitude);
                                        NewBorehole.BHSamples = new List<Models.BHSample>();
                                        NewBorehole.TransectName = "Transect 1";
                                        NewBorehole.Station = Convert.ToDouble(theStation);
                                        NewBorehole.Elevation = Convert.ToDouble(theElevation);

                                        BHChanged = true;

                                        DBAssessment.Boreholes.Add(NewBorehole);
                                        db.Entry(DBAssessment).State = System.Data.Entity.EntityState.Modified;
                                        db.Entry(NewBorehole).State = System.Data.Entity.EntityState.Added;

                                        //relate the borehole to a transect
                                        foreach (Transect thisT in DBAssessment.Transects)
                                        {
                                            if (thisT.TransectName.Trim() == CurrentTransectName.Trim())
                                            {
                                                NewBorehole.TransectName = thisT.TransectName;
                                                if (thisT.Boreholes == null)
                                                {
                                                    thisT.Boreholes = new List<BloodDonors.Models.Borehole>();
                                                }
                                                thisT.Boreholes.Add(NewBorehole);
                                                //db.Entry(thisT).State = System.Data.Entity.EntityState.Modified;
                                                break;
                                            }
                                        }

                                        //also add the samples
                                        var lastGMC = "";
                                        for (int k = 0; k < BHSamples.Length; k++)
                                        {
                                            var thisSample = BHSamples[k].Split(',');

                                            if (thisSample.Length >= 4)
                                            {
                                                string thisSampleName = thisSample[0].Trim();
                                                if (thisSampleName == NewBorehole.BoreholeName)
                                                {
                                                    BloodDonors.Models.BHSample NewSample = new BloodDonors.Models.BHSample();
                                                    NewSample.BoreholdID = NewBorehole.BoreholeID;

                                                    NewSample.Depth = Convert.ToDouble(thisSample[1]);
                                                    NewSample.Material = thisSample[2];
                                                    NewSample.PF_code = thisSample[3];

                                                    if (thisSample.Length >= 5)
                                                    {
                                                        if (thisSample[4] != "")
                                                        {
                                                            if (thisSample[4] == "ICE")
                                                            {
                                                                NewSample.GMC = 1000;
                                                            } else  {
                                                                NewSample.GMC = Convert.ToDouble(thisSample[4]);
                                                            }
                                                            lastGMC = Convert.ToString(NewSample.GMC);
                                                        }
                                                        else if (lastGMC != "")
                                                        {
                                                            NewSample.GMC = Convert.ToDouble(lastGMC);
                                                        }
                                                        else
                                                        {
                                                            NewSample.GMC = 0;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        NewSample.GMC = 0;
                                                    }

                                                   

                                                    NewBorehole.BHSamples.Add(NewSample);
                                                    db.Entry(NewSample).State = System.Data.Entity.EntityState.Added;
                                                }


                                            }
                                        }

                                        //db.Transects.Add(NewTransect);

                                    }
                                }

                                

                            }


                        }

                        EvaluateStability(DBAssessment);

                        DBAssessment.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();

                        if (BHChanged || TransectChanged)
                        {
                            //return RedirectToAction("Open", new { id = DBAssessment.AssessmentID });



                            return Json(DBAssessment, JsonRequestBehavior.AllowGet);


                        } else
                        {
                            //return null;
                            return Json(DBAssessment, JsonRequestBehavior.AllowGet);
                        }
                        

                    } else
                    {
                        return null;
                    }
            



                    //if (DBAssessment != null)
                    //{



                    //    db.SaveChanges();
                    //}


                    //ModelStateDictionary state = ModelState;
                    //ModelState statevalue;
                    //string AttemptValue = "";
                    //if (state.TryGetValue("AssessmentName", out statevalue))
                    //{
                    //    AttemptValue = statevalue.Value.AttemptedValue.ToString();
                    //}


                    ////foreach (Assessment thisassessemnt in db.Assessments)
                    ////{
                    ////    if (thisassessemnt.AssessmentID == theAssessment.AssessmentID )
                    ////    {
                    ////        thisassessemnt.AssessmentName = theAssessment.AssessmentName;
                    ////    }
                    ////}


                    //db.SaveChanges();

                    //// return RedirectToAction("Index");


                }

                catch (DbEntityValidationException dbEx)

                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }

                    return null;

                }


            //return View("Complete", theAssment);

            return null;

        }



        public ActionResult ClearState()
        {

          //  ModelState.Clear();
            return null;

        }




    }

    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetExecutionStrategy(
                "System.Data.SqlClient",
                () => new SqlAzureExecutionStrategy(1, TimeSpan.FromSeconds(30)));
        }
    }


    class AGOL
    {
        private string _token;
        private string _username;
        private string _password;
        public organizationInformation orgInfo;


        public string Token
        {
            get
            {
                return _token;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public AGOL(string UserName, string PassWord)
        {
            _username = UserName;
            _password = PassWord;
            _token = GetToken(UserName, PassWord);
            //orgInfo = _getOrgInfo(_token);
        }



        public string GetToken(string ClientID, string ClientSecret)
        {

            //var data = new NameValueCollection();
            //data["username"] = Username;
            //data["password"] = Password;
            //data["referer"] = "https://www.arcgis.com";
            //data["f"] = "json";

            //TokenInfo x = JsonConvert.DeserializeObject<TokenInfo>(_getResponse(data, "https://arcgis.com/sharing/rest/generateToken"));


            var data = new NameValueCollection();
            data["client_id"] = ClientID;
            data["client_secret"] = ClientSecret;
            data["grant_type"] = "client_credentials";

            TokenInfo x = JsonConvert.DeserializeObject<TokenInfo>(_getResponse(data, "https://www.arcgis.com/sharing/rest/oauth2/token/"));

            return x.access_token;
        }

        private organizationInformation _getOrgInfo(string token)
        {
            var data = new NameValueCollection();
            data["token"] = token;
            data["f"] = "json";

            organizationInformation x = JsonConvert.DeserializeObject<organizationInformation>(_getResponse(data, "http://www.arcgis.com/sharing/rest/portals/self"));
            return x;
        }

        private string _getResponse(NameValueCollection data, string url)
        {
            string responseData;
            var webClient = new WebClient();
            var response = webClient.UploadValues(url, data);
            responseData = System.Text.Encoding.UTF8.GetString(response);
            return responseData;
        }

        //Starts the collection of useless classes....
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        public class TokenInfo
        {
            public string access_token { get; set; }
            public long expires_in { get; set; }
            //public bool ssl { get; set; }
        }

        public class organizationInformation
        {
            public string access { get; set; }
            public bool allSSL { get; set; }
            public double availableCredits { get; set; }
            public string backgroundImage { get; set; }
            public string basemapGalleryGroupQuery { get; set; }
            public string bingKey { get; set; }
            public bool canListApps { get; set; }
            public bool canListData { get; set; }
            public bool canListPreProvisionedItems { get; set; }
            public bool canProvisionDirectPurchase { get; set; }
            public bool canSearchPublic { get; set; }
            public bool canShareBingPublic { get; set; }
            public bool canSharePublic { get; set; }
            public bool canSignInArcGIS { get; set; }
            public bool canSignInIDP { get; set; }
            public string colorSetsGroupQuery { get; set; }
            public bool commentsEnabled { get; set; }
            public long created { get; set; }
            public string culture { get; set; }
            public string customBaseUrl { get; set; }
            public int databaseQuota { get; set; }
            public int databaseUsage { get; set; }
            public string description { get; set; }
            public string featuredGroupsId { get; set; }
            public string featuredItemsGroupQuery { get; set; }
            public string galleryTemplatesGroupQuery { get; set; }
            public string helpBase { get; set; }
            public string homePageFeaturedContent { get; set; }
            public int homePageFeaturedContentCount { get; set; }
            public int httpPort { get; set; }
            public int httpsPort { get; set; }
            public string id { get; set; }
            public string ipCntryCode { get; set; }
            public bool isPortal { get; set; }
            public string layerTemplatesGroupQuery { get; set; }
            public int maxTokenExpirationMinutes { get; set; }
            public long modified { get; set; }
            public string name { get; set; }
            public string portalHostname { get; set; }
            public string portalMode { get; set; }
            public string portalName { get; set; }
            public object portalThumbnail { get; set; }
            public string region { get; set; }
            public bool showHomePageDescription { get; set; }
            public string staticImagesUrl { get; set; }
            public long storageQuota { get; set; }
            public long storageUsage { get; set; }
            public bool supportsHostedServices { get; set; }
            public bool supportsOAuth { get; set; }
            public string symbolSetsGroupQuery { get; set; }
            public string templatesGroupQuery { get; set; }
            public string thumbnail { get; set; }
            public string units { get; set; }
            public string urlKey { get; set; }
            public bool useStandardizedQuery { get; set; }
        }
    }


}
