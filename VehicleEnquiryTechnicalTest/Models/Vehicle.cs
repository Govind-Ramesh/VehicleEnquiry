using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleEnquiryTechnicalTest.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Chassis { get; set; }
        public DateTime DateCreated { get; set; }
        public int ColourId { get; set; }
        public int GearBoxId { get; set; }
        public int CarTypeId { get; set; }
    }

}