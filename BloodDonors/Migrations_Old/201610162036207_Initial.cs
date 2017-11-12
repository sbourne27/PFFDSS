namespace BloodDonors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessment",
                c => new
                    {
                        AssessmentID = c.Int(nullable: false, identity: true),
                        AssessmentName = c.String(nullable: false),
                        StructureType = c.Int(nullable: false),
                        LightStructureUse = c.Int(nullable: false),
                        HeavyStructureUse = c.Int(nullable: false),
                        LinearStructureUse = c.Int(nullable: false),
                        CurrentStep = c.String(nullable: false),
                        Stories = c.Int(nullable: false),
                        FoundationElevation = c.Int(nullable: false),
                        LifeSafetyRequirement = c.Int(nullable: false),
                        AcceptableFoundationFailureRiskLevel = c.Int(nullable: false),
                        StructureHeated = c.Int(nullable: false),
                        ObservedIceWedges = c.Int(nullable: false),
                        TopographyEstimate = c.Int(nullable: false),
                        ThermoKarstPresent = c.Int(nullable: false),
                        VegetationType = c.Int(nullable: false),
                        BlackSprucePresent = c.Int(nullable: false),
                        DrunkenForestPresent = c.Int(nullable: false),
                        StreamsPresent = c.Int(nullable: false),
                        MarshesStandingWaterPresent = c.Int(nullable: false),
                        ExistingStructuresPresent = c.Int(nullable: false),
                        ExistingStructureType = c.Int(nullable: false),
                        ExistingStructuresHeated = c.Int(nullable: false),
                        ExistingStructuresFoundationCondition = c.Int(nullable: false),
                        SiteLatitude = c.Double(nullable: false),
                        SiteLongitude = c.Double(nullable: false),
                        AssessmentArea = c.Double(nullable: false),
                        StructureLength = c.Double(nullable: false),
                        StructureWidth = c.Double(nullable: false),
                        LifeSpan = c.Int(nullable: false),
                        MT = c.Int(nullable: false),
                        IC = c.Int(nullable: false),
                        V = c.Int(nullable: false),
                        R = c.Int(nullable: false),
                        C = c.Int(nullable: false),
                        DDF = c.Int(nullable: false),
                        ShapeCoordinates = c.String(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AssessmentID);
            
            CreateTable(
                "dbo.Borehole",
                c => new
                    {
                        BoreholeID = c.Int(nullable: false, identity: true),
                        BoreholeName = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Elevation = c.Double(nullable: false),
                        Transect = c.String(nullable: false),
                        Station = c.Double(nullable: false),
                        LeftSwathPointX = c.Double(nullable: false),
                        LeftSwathPointY = c.Double(nullable: false),
                        RightSwathPointX = c.Double(nullable: false),
                        RightSwathPointY = c.Double(nullable: false),
                        AssessmentID = c.Int(nullable: false),
                        Transect_TransectId = c.Int(),
                    })
                .PrimaryKey(t => t.BoreholeID)
                .ForeignKey("dbo.Assessment", t => t.AssessmentID, cascadeDelete: true)
                .ForeignKey("dbo.Transect", t => t.Transect_TransectId)
                .Index(t => t.AssessmentID)
                .Index(t => t.Transect_TransectId);

            CreateTable(
                "dbo.BHSample",
                c => new
                {
                    BHSampleId = c.Int(nullable: false, identity: true),
                    Depth = c.Double(nullable: false),
                    Material = c.String(nullable: false),
                    PF_code = c.String(nullable: false),
                    GMC = c.Double(nullable: false),
                    Stability = c.Double(nullable: false),
                    BoreholdID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.BHSampleId)
                .ForeignKey("dbo.Borehole", t => t.BoreholdID, cascadeDelete: true)
                .Index(t => t.BoreholdID);


            CreateTable(
                "dbo.Transect",
                c => new
                {
                    TransectId = c.Int(nullable: false, identity: true),
                    TransectName = c.String(nullable: false),
                    AssessmentID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TransectId)
                .ForeignKey("dbo.Assessment", t => t.AssessmentID, cascadeDelete: true)
                .Index(t => t.AssessmentID);
            
            CreateTable(
                "dbo.TransectImage",
                c => new
                    {
                        TransectImageId = c.Int(nullable: false, identity: true),
                        TransectImageName = c.String(nullable: false),
                        TransectImageAlt = c.String(nullable: false),
                        ImageData = c.Binary(nullable: false),
                        ContentType = c.String(nullable: false),
                        StartStation = c.Double(nullable: false),
                        EndStation = c.Double(nullable: false),
                        Type = c.String(nullable: false),
                        TransectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransectImageId)
                .ForeignKey("dbo.Transect", t => t.TransectID, cascadeDelete: true)
                .Index(t => t.TransectID);
            
            CreateTable(
                "dbo.TransectPoint",
                c => new
                    {
                        TransectPointID = c.Int(nullable: false, identity: true),
                        PointName = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Elevation = c.Double(nullable: false),
                        Station = c.Double(nullable: false),
                        TransectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransectPointID)
                .ForeignKey("dbo.Transect", t => t.TransectID, cascadeDelete: true)
                .Index(t => t.TransectID);
            
            CreateTable(
                "dbo.Donor",
                c => new
                    {
                        DonorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        bGroup = c.String(),
                        Description = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DonorID);

           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transect", "AssessmentID", "dbo.Assessment");
            DropForeignKey("dbo.TransectPoint", "TransectID", "dbo.Transect");
            DropForeignKey("dbo.TransectImage", "TransectID", "dbo.Transect");
            DropForeignKey("dbo.Borehole", "Transect_TransectId", "dbo.Transect");
            DropForeignKey("dbo.Borehole", "AssessmentID", "dbo.Assessment");
            DropIndex("dbo.TransectPoint", new[] { "TransectID" });
            DropIndex("dbo.TransectImage", new[] { "TransectID" });
            DropIndex("dbo.Transect", new[] { "AssessmentID" });
            DropIndex("dbo.Borehole", new[] { "Transect_TransectId" });
            DropIndex("dbo.Borehole", new[] { "AssessmentID" });
            DropTable("dbo.Donor");
            DropTable("dbo.TransectPoint");
            DropTable("dbo.TransectImage");
            DropTable("dbo.Transect");
            DropTable("dbo.BHSample");
            DropTable("dbo.Borehole");
            DropTable("dbo.Assessment");
        }
    }
}
