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
    public class VehicleEnquiryController : Controller
    {

        private VehDBContext db = new VehDBContext();

        // GET: VehicleEnquiry
        public ActionResult Index()
        {
            //To fetch the list of registered vehicles
            var vehicles = from e in db.Vehicles
                           orderby e.ID
                           select e;
            return View(vehicles);
        }

        // GET: VehicleEnquiry/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleEnquiry/Create
        public ActionResult Create()
        {
            //Get the values from DB to populate in the create vehicle dropdown lists
            try
            {
                CreateVehicles objCreateVeh = new CreateVehicles();

                objCreateVeh.CarTypes = (from cartype in db.VehicleCarType
                                         where cartype.Active == true
                                         orderby cartype.ID
                                         select cartype).ToList();

                objCreateVeh.Colours = (from color in db.VehicleColour
                                        where color.Active == true
                                        orderby color.ID
                                        select color).ToList();

                objCreateVeh.GearTypes = (from gear in db.VehicleGearbox
                                          where gear.Active == true
                                          orderby gear.ID
                                          select gear).ToList();
                return View(objCreateVeh);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: VehicleEnquiry/Create
        [HttpPost]
        public ActionResult Create(CreateVehicles veh, FormCollection Fcollection)
        {
            //Create vehicle 
            try
            {
                Vehicle addVehDetails = new Vehicle();
                addVehDetails.Chassis = veh.Chassis;
                addVehDetails.DateCreated = DateTime.Now;
                addVehDetails.CarTypeId = veh.CarTypeID;
                addVehDetails.ColourId = veh.ColourID;
                addVehDetails.GearBoxId = veh.GearID;
                db.Vehicles.Add(addVehDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: VehicleEnquiry/Edit/5
        public ActionResult Edit(int id)
        {
            var vehicle = db.Vehicles.Single(m => m.ID == id);
            return View(vehicle);
        }

        // POST: VehicleEnquiry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //Edit the selected record 
            try
            {
                var vehicle = db.Vehicles.Single(m => m.ID == id);
                if (TryUpdateModel(vehicle))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(vehicle);
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleEnquiry/Delete/5
        public ActionResult Delete(int? id)
        {
            //Check if the selected record is available for the delete
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle veh = db.Vehicles.Find(id);

            if (veh == null)
            {
                return HttpNotFound();
            }
            return View(veh);
        }

        // POST: VehicleEnquiry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete the selected vehicle from the list
            Vehicle veh = db.Vehicles.Find(id);
            db.Vehicles.Remove(veh);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
