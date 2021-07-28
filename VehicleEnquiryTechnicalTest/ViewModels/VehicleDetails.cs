using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleEnquiryTechnicalTest.ViewModels
{
    public class VehicleDetails
    {
        public string Chassis { get; set; }
        public DateTime DateCreated { get; set; }
        public string Colour { get; set; }
        public string GearBox { get; set; }
        public string CarType { get; set; }
    }
}