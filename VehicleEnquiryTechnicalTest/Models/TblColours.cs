using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleEnquiryTechnicalTest.Models
{
    public class TblColours
    {
        public int ID { get; set; }
        public string Colours { get; set; }
        public bool Active { get; set; }
    }

}