using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonors.Models
{

    public enum StructureType
    {
        Light = 1,
        Heavy = 2,
        Barracks_Large_Transient = 4,
        Barracks_Large_Permanent = 5,
        Barracks_Small = 6,
        Government_Admin = 7,
        Educational_Small = 8,
        Educational_Large = 9,
        Public_Gathering_Facility_Small = 10,
        Public_Gathering_Facility_Large = 11,
        Church_Cemetery_Small = 12,
        Church_Cemetery_Large = 13,
        Canteen_Small = 14,
        Canteen_Large = 15,
        Commissary = 16,
        Office_Small = 17,
        Custodial_Small = 18,
        Custodial_Medium = 19,
        Custodial_Large = 20,
        Hospital = 21,
        Brig = 22,
        Power_Plant = 23,
        Nuclear_Power_Plant = 24,
        Water_Treatment_Plant = 25,
        Wastewater_Treatment_Plant = 26,
        Television_Comm_Station = 27,
        Cell_Tower = 28,
        Manufacturing_LowHazard = 29,
        Manufacturing_Moderate_Hazard = 30,
        Manufacturing_High_Hazard = 31,
        Utility_Moderate_Hazard = 32,
        Storage_Small = 33,
        Storage_Large = 34,
        Storage_High_Hazard = 35,
        Warehouse_Low_Hazard = 36,
        Warehouse_Moderate_Hazard = 37,
        Warehouse_High_Hazard = 38,
        Hangar = 39,
        Ammunition_Depot = 40,
        Multiple_Use = 41
        //Linear = 3
    }

    public enum LightStructureUse
    {
        Warehouse = 1,
        Residential = 2,
        Offices = 3,
    }

    public enum HeavyStructureUse
    {
        Hospital = 1,
        Powerplant = 2,
    }

    public enum LinearStructureUse
    {
        Road = 1,
        Pipeline = 2,
        Telecom = 3
    }

    public enum FoundationElevation
    {
        Elevated = 1,
        Grade = 2,
    }

    public enum LifeSafetyRequirement
    {
        High = 1,
        Med = 2,
        Low = 3,
    }

    public enum AcceptableFoundationFailureRiskLevel
    {
        High = 1,
        Med = 2,
        Low = 3,
    }

    public enum StructureHeated
    {
        Heated = 1,
        NotHeated = 2,
    }

    public enum ObservedIceWedges
    {
        IceWedges = 1,
        NoIceWedges = 2,
    }

    public enum TopographyEstimate
    {
        Flat = 1,
        Moderate = 2,
        Steep = 3,
    }

    public enum ThermoKarstPresent
    {
        ThermoKarstPresent = 1,
        NoThermoKarstPresent = 2,
    }

    public enum VegetationType
    {
        LowElevationBlackSpruce = 1,
        HighElevationBlackSpruce = 2,
        WhiteSpruce = 3,
        Deciduous = 4,
        Tussock = 5,
        EarlySuccessionBorealForest = 6,
    }

    public enum BlackSprucePresent
    {
        BlackSprucePresent = 1,
        BlackSpruceNotPresent = 2,
    }

    public enum DrunkenForestPresent
    {
        DrunkenForestPresent = 1,
        DrunkenForestNotPresent = 2,
    }

    public enum MarshesStandingWaterPresent
    {
        MarshesStandingWaterPresentPresent = 1,
        MarshesStandingWaterPresentNotPresent = 2,
    }

    public enum ExistingStructuresPresent
    {
        ExistingStructuresPresentPresent = 1,
        ExistingStructuresPresentNotPresent = 2,
    }

    public enum ExistingStructuresTypes
    {
        Light = 1,
        Heavy = 2,
        Linear = 3,
    }
    public enum ExistingStructuresHeated
    {
        ExistingStructuresHeated = 1,
        ExistingStructuresNotHeated = 2,
    }
    public enum ExistingStructuresFoundationCondition
    {
        Good = 1,
        Failing = 2,
        Failed = 3
    }

    public enum ExistingStructureType
    {
        Light = 1,
        Heavy = 2,
        Linear = 3,
        Barracks_Large_Transient = 4,
        Barracks_Large_Permanent = 5,
        Barracks_Small = 6,
        Government_Admin = 7,
        Educational_Small = 8,
        Educational_Large = 9,
        Public_Gathering_Facility_Small = 10,
        Public_Gathering_Facility_Large = 11,
        Church_Cemetery_Small = 12,
        Church_Cemetery_Large = 13,
        Canteen_Small = 14,
        Canteen_Large = 15,
        Commissary = 16,
        Office_Small = 17,
        Custodial_Small = 18,
        Custodial_Medium = 19,
        Custodial_Large = 20,
        Hospital = 21,
        Brig = 22,
        Power_Plant = 23,
        Nuclear_Power_Plant = 24,
        Water_Treatment_Plant = 25,
        Wastewater_Treatment_Plant = 26,
        Television_Comm_Station = 27,
        Cell_Tower = 28,
        Manufacturing_LowHazard = 29,
        Manufacturing_Moderate_Hazard = 30,
        Manufacturing_High_Hazard = 31,
        Utility_Moderate_Hazard = 32,
        Storage_Small = 33,
        Storage_Large = 34,
        Storage_High_Hazard = 35,
        Warehouse_Low_Hazard = 36,
        Warehouse_Moderate_Hazard = 37,
        Warehouse_High_Hazard = 38,
        Hangar = 39,
        Ammunition_Depot = 40,
        Multiple_Use = 41
    }

    public enum StreamsPresent
    {
        StreamsPresent = 1,
        StreamsNotPresent = 2,
    }


    public class Assessment
    {
        //primary key
        public int AssessmentID { get; set; }

        [Required]
        [Display(Name = "Assessment Name")]
        public string AssessmentName { get; set; }

        [UIHint("RadioButtonList")]
        [Required]
        [Display(Name = "Structure Type")]
        public StructureType StructureType { get; set; }

        [Required]
        [Display(Name = "Light Structure Use")]
        public LightStructureUse LightStructureUse { get; set; }

        [Required]
        [Display(Name = "Heavy Structure Use")]
        public HeavyStructureUse HeavyStructureUse { get; set; }

        [Required]
        [Display(Name = "Linear Structure Use")]
        public LinearStructureUse LinearStructureUse { get; set; }

        [Required]
        [Display(Name = "Current Step")]
        public string CurrentStep { get; set; }

        [Required]
        [Display(Name = "Stories")]
        public int Stories { get; set; }

        [UIHint("RadioButtonList")]
        [Required]
        [Display(Name = "Foundation Elevation")]
        public FoundationElevation FoundationElevation { get; set; }

        [Required]
        [Display(Name = "Life Safety Requirement")]
        public LifeSafetyRequirement LifeSafetyRequirement { get; set; }

        [Required]
        [Display(Name = "Acceptable Foundation Failure Risk Level")]
        public AcceptableFoundationFailureRiskLevel AcceptableFoundationFailureRiskLevel { get; set; }

        [Required]
        [Display(Name = "Structure Heated")]
        public StructureHeated StructureHeated { get; set; }

        [Required]
        [Display(Name = "Observed Ice Wedges")]
        public ObservedIceWedges ObservedIceWedges { get; set; }

        [Required]
        [Display(Name = "Topography Estimate")]
        public TopographyEstimate TopographyEstimate { get; set; }

        [Required]
        [Display(Name = "ThermoKarst Present")]
        public ThermoKarstPresent ThermoKarstPresent { get; set; }

        [Required]
        [Display(Name = "Vegetation Type")]
        public VegetationType VegetationType { get; set; }

        [Required]
        [Display(Name = "Black Spruce Present")]
        public BlackSprucePresent BlackSprucePresent { get; set; }

        [Required]
        [Display(Name = "Drunken Forest Present")]
        public DrunkenForestPresent DrunkenForestPresent { get; set; }

        [Required]
        [Display(Name = "Streams Present")]
        public StreamsPresent StreamsPresent { get; set; }

        [Required]
        [Display(Name = "Marshes and/or Standing Water Present")]
        public MarshesStandingWaterPresent MarshesStandingWaterPresent { get; set; }

        [Required]
        [Display(Name = "Existing Structures Present")]
        public ExistingStructuresPresent ExistingStructuresPresent { get; set; }

        [Required]
        [Display(Name = "Existing Structure Type")]
        public ExistingStructureType ExistingStructureType { get; set; }

        [Required]
        [Display(Name = "Existing Structures Heated")]
        public ExistingStructuresHeated ExistingStructuresHeated { get; set; }

        [Required]
        [Display(Name = "Existing Structures Foundation Condition")]
        public ExistingStructuresFoundationCondition ExistingStructuresFoundationCondition { get; set; }

        [Required]
        [Display(Name = "Site Latitude")]
        public double SiteLatitude { get; set; }

        [Required]
        [Display(Name = "Site Longitude")]
        public double SiteLongitude { get; set; }

        [Required]
        [Display(Name = "Site Angle")]
        public double SiteAngle { get; set; }

        [Required]
        [Display(Name = "Assessment Area")]
        public double AssessmentArea { get; set; }

        [Required]
        [Display(Name = "Structure Length [m]")]
        public double StructureLength { get; set; }

        [Required]
        [Display(Name = "Structure Width [m]")]
        public double StructureWidth { get; set; }

        [Required]
        [Display(Name = "Life Span")]
        public int LifeSpan { get; set; }

        [Required]
        [Display(Name = "Material Type")]
        public int MT { get; set; }

        [Required]
        [Display(Name = "Ice Content")]
        public int IC { get; set; }

        [Required]
        [Display(Name = "Vegetation Type")]
        public int V { get; set; }

        [Required]
        [Display(Name = "Required Resiliency")]
        public int R { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public int C { get; set; }

        [Required]
        [Display(Name = "Development Difficulty Factor")]
        public int DDF { get; set; }

        [Required]
        [Display(Name = "ShapeCoordinates")]
        public string ShapeCoordinates { get; set; }

        [Required]
        [Display(Name = "LastModifiedDate")]
        public DateTime? LastModifiedDate { get; set; }

        [Required]
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [Display(Name = "UserID")]
        public Guid UserID { get; set; }


        public virtual ICollection<Borehole> Boreholes { get; set; }

        public virtual ICollection<Transect> Transects { get; set; }


    }
}