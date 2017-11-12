using System;
using System.Collections.Generic;
using System.Data.Entity;
using BloodDonors.Models;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace BloodDonors.Models
{
    public class DonorDBContext : DbContext
    {

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Borehole> Boreholes { get; set; }
        public DbSet<BHSample> BHSamples { get; set; }
        public DbSet<Transect> Transects { get; set; }
        public DbSet<TransectPoint> TransectPoints { get; set; }
        public DbSet<TransectImage> TransectImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           
        }
    }

}
