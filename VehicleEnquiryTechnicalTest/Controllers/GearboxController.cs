using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleEnquiryTechnicalTest.Models;

namespace VehicleEnquiryTechnicalTest.Controllers
{
    public class GearboxController : Controller
    {

        private VehDBContext db = new VehDBContext();

        // GET: Gearbox
        public ActionResult GearIndex()
        {
            //get the list of gearboxes available in the DB
            var gearboxtype = from e in db.VehicleGearbox
                              where e.Active == true
                              orderby e.ID
                              select e;
            return View(gearboxtype);
        }

        // GET: Gearbox/Details/5
        public ActionResult GearDetails(int id)
        {
            return View();
        }

        // GET: Gearbox/Create
        public ActionResult GearCreate()
        {
            return View();
        }

        // POST: Gearbox/Create
        [HttpPost]
        public ActionResult GearCreate(Gearbox gears)
        {
            //check if the gear type is already available. If available but not active then make the Geartype active
            //If not available then create the geartype
            List<Gearbox> checkGear = (from g in db.VehicleGearbox
                                       where g.GearboxType == gears.GearboxType
                                       select g).ToList();

            if (checkGear.Count != 0)
            {
                foreach (Gearbox gb in checkGear)
                {
                    gb.Active = true;
                    db.SaveChanges();
                }
            }
            else
            {
                gears.Active = true;
                db.VehicleGearbox.Add(gears);
                db.SaveChanges();
            }

            return RedirectToAction("GearIndex");
        }

        // GET: Gearbox/Edit/5
        public ActionResult GearEdit(int id)
        {
            var gearboxtype = db.VehicleGearbox.Single(m => m.ID == id);
            return View(gearboxtype);
        }

        // POST: Gearbox/Edit/5
        [HttpPost]
        public ActionResult GearEdit(int id, FormCollection collection)
        {
            try
            {
                var gearboxtype = db.VehicleGearbox.Single(m => m.ID == id);
                if (TryUpdateModel(gearboxtype))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("GearIndex");
                }
                return View(gearboxtype);
            }
            catch
            {
                return View();
            }
        }

        // GET: Gearbox/Delete/5
        public ActionResult GearDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gearbox g = db.VehicleGearbox.Find(id);

            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        // POST: Gearbox/Delete/5
        [HttpPost, ActionName("GearDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GearDeleteSave(int id)
        {
            // Get the selected record from DB
            List<Gearbox> getGear = (from gear in db.VehicleGearbox
                                     where gear.ID == id
                                     select gear).ToList();

            foreach (Gearbox g in getGear)
            {
                //update the Gear's active status to false to make the gear inactive in further selections
                //But not completely removed
                g.Active = false;
            }

            db.SaveChanges();
            return RedirectToAction("GearIndex");
        }
    }
}
