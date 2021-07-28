using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using VehicleEnquiryTechnicalTest.Models;

namespace VehicleEnquiryTechnicalTest.Controllers
{
    public class CarTypeController : Controller
    {
        private VehDBContext db = new VehDBContext();

        // GET: CarType
        public ActionResult CarType()
        {
            //get the list of gearboxes available in the DB
            var listCarTypes = from ct in db.VehicleCarType
                               where ct.Active == true
                               orderby ct.ID
                               select ct;

            return View(listCarTypes);
        }

        public ActionResult CarTypeCreate()
        {
            return View();
        }

        // POST: CarType/Create
        [HttpPost]
        public ActionResult CarTypeCreate(CarTypes carTypes)
        {
            //check if the car type is already available. If available but not active then make the cartype active
            //If not available then create the cartype
            List<CarTypes> checkCarTypes = (from ct in db.VehicleCarType
                                       where ct.CarType == carTypes.CarType
                                       select ct).ToList();

            if (checkCarTypes.Count != 0)
            {
                foreach (CarTypes gb in checkCarTypes)
                {
                    gb.Active = true;
                    db.SaveChanges();
                }
            }
            else
            {
                carTypes.Active = true;
                db.VehicleCarType.Add(carTypes);
                db.SaveChanges();
            }

            return RedirectToAction("CarType");
        }

        public ActionResult CarTypeEdit(int id)
        {
            var checkCarType = db.VehicleCarType.Single(m => m.ID == id);
            return View(checkCarType);
        }

        // POST: CarType/Edit
        [HttpPost]
        public ActionResult CarTypeEdit(int id, FormCollection collection)
        {
            try
            {
                var getCarType = db.VehicleCarType.Single(m => m.ID == id);
                if (TryUpdateModel(getCarType))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("CarType");
                }
                return View(getCarType);
            }
            catch
            {
                return View();
            }
        }

        // GET: CarType/Delete/
        public ActionResult CarTypeDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarTypes ct = db.VehicleCarType.Find(id);

            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Gearbox/Delete/5
        [HttpPost, ActionName("CarTypeDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CarTypeDeleteSave(int id)
        {
            // Get the selected record from DB
            List<CarTypes> getCarTypes = (from crtyp in db.VehicleCarType
                                     where crtyp.ID == id
                                     select crtyp).ToList();

            foreach (CarTypes c in getCarTypes)
            {
                //update the CarType's active status to false to make the CarType inactive in further selections
                //But not completely removed
                c.Active = false;
            }

            db.SaveChanges();
            return RedirectToAction("CarType");
        }
    }
}