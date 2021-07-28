using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleEnquiryTechnicalTest.Models;

namespace VehicleEnquiryTechnicalTest.ViewModels
{
    public class CreateVehicles
    {
        public string Chassis { get; set; }
        public DateTime DateCreated { get; set; }

        public int CarTypeID { get; set; }
        public List<CarTypes> CarTypes { get; set; }

        public int ColourID { get; set; }
        public List<TblColours> Colours { get; set; }

        public int GearID { get; set; }
        public List<Gearbox> GearTypes { get; set; }

    }
}