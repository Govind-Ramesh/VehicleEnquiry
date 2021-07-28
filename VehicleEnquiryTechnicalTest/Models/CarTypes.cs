using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleEnquiryTechnicalTest.Models
{
    public class CarTypes
    {
        public int ID { get; set; }
        public string CarType { get; set; }
        public bool Active { get; set; }

    }
}