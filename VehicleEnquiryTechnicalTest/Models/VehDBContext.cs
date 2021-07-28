using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace VehicleEnquiryTechnicalTest.Models
{
    public class VehDBContext : DbContext
    {
        public VehDBContext()
        {

        }
        public DbSet<Vehicle> Vehicles
        {
            get; set;
        }

        public DbSet<TblColours> VehicleColour
        {
            get; set;
        }

        public DbSet<Gearbox> VehicleGearbox
        {
            get; set;
        }

        public DbSet<CarTypes> VehicleCarType
        {
            get; set;
        }
    }
}