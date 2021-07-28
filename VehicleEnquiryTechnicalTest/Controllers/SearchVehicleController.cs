using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleEnquiryTechnicalTest.Models;
using VehicleEnquiryTechnicalTest.ViewModels;

namespace VehicleEnquiryTechnicalTest.Controllers
{
    public class SearchVehicleController : Controller
    {
        private VehDBContext db = new VehDBContext();

        // GET: SearchVehicle
        public ActionResult Index()
        {
            return View("SearchVehicles");
        }

        public ActionResult SearchVehicles(string chassisNumber)
        {
            //The Vehicle table holds the IDs of Colour, gearbox and CarType. So join the other tables
            //and fetch the appropriate values to show it on the search screen

            var vehDetailList = from vehicle in db.Vehicles
                                join type in db.VehicleCarType on vehicle.CarTypeId equals type.ID
                                join color in db.VehicleColour on vehicle.ColourId equals color.ID
                                join gear in db.VehicleGearbox on vehicle.GearBoxId equals gear.ID
                                where vehicle.Chassis == chassisNumber
                                select new VehicleDetails()
                                {
                                    Chassis = vehicle.Chassis,
                                    CarType = type.CarType,
                                    Colour = color.Colours,
                                    GearBox = gear.GearboxType,
                                    DateCreated = vehicle.DateCreated
                                };

            return View(vehDetailList);

        }
    }
}