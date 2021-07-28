using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleEnquiryTechnicalTest.Models
{
    public class Gearbox
    {
        public int ID { get; set; }
        public string GearboxType { get; set; }
        public bool Active { get; set; }
    }
}