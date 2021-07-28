using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleEnquiryTechnicalTest.Models;

namespace VehicleEnquiryTechnicalTest.Controllers
{
    public class ColourController : Controller
    {

        private VehDBContext db = new VehDBContext();

        // GET: Colour
        public ActionResult Colour()
        {
            //get the list of gearboxes available in the DB
            var colour = from c in db.VehicleColour
                         where c.Active == true
                         orderby c.ID
                         select c;
            return View(colour);
        }

        // GET: Colour/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Colour/Create
        public ActionResult ColourCreate()
        {
            return View();
        }

        // POST: Colour/Create
        [HttpPost]
        public ActionResult ColourCreate(TblColours colour)
        {
            //check if the colour is already available. If available but not active then make the colour active
            //If not available then create the colour
            try
            {
                List<TblColours> checkColour = (from c in db.VehicleColour
                                                where c.Colours == colour.Colours
                                                select c).ToList();

                if (checkColour.Count != 0)
                {
                    foreach (TblColours c in checkColour)
                    {
                        c.Active = true;
                        db.SaveChanges();
                    }
                }
                else
                {
                    colour.Active = true;
                    db.VehicleColour.Add(colour);
                    db.SaveChanges();
                }

                return RedirectToAction("Colour");
            }

            catch
            {
                return View();
            }
        }

        // GET: Colour/Edit/5
        public ActionResult ColourEdit(int id)
        {
            var colour = db.VehicleColour.Single(m => m.ID == id);
            return View(colour);
        }

        // POST: Colour/Edit/5
        [HttpPost]
        public ActionResult ColourEdit(int id, FormCollection collection)
        {
            try
            {
                var colour = db.VehicleColour.Single(m => m.ID == id);
                if (TryUpdateModel(colour))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("Colour");
                }
                return View(colour);
            }
            catch
            {
                return View();
            }
        }

        // GET: Colour/Delete/5
        public ActionResult ColourDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblColours col = db.VehicleColour.Find(id);

            if (col == null)
            {
                return HttpNotFound();
            }
            return View(col);
        }

        // POST: Colour/Delete/5
        [HttpPost, ActionName("ColourDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Get the selected record from DB
            List<TblColours> getColour = (from c in db.VehicleColour
                                          where c.ID == id
                                          select c).ToList();

            foreach (TblColours p in getColour)
            {
                //update the colour's active status to false to make the color inactive
                //But not completely removed
                p.Active = false;
            }

            db.SaveChanges();
            return RedirectToAction("Colour");

        }

    }
}
