using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KrinPark.Models;
using Microsoft.AspNet.Identity;

namespace KrinPark.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
      //  [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var vehicles = db.Vehicles.Include(v => v.Driver);
                return View(vehicles.ToList());
            }
            else
            {
                return MyVehicles();
            }
           
        }

        public ActionResult MyVehicles()
        {
            var userName = User.Identity.Name;// fetches currently logged in user
            //now lets get the vehicles for this user.
            var vehicles = db.Vehicles.Include(v => v.Driver).Where(x=>x.Driver.ApplicationUser.UserName== userName);
            return View("Index",vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegNo")] Vehicle vehicle)
        {
            
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                vehicle.DriverId = db.Drivers.Where(x => x.ApplicationUserId == userID).FirstOrDefault().DriverId;
                vehicle.VehicleId = Guid.NewGuid();
                vehicle.CreatedBy = User.Identity.Name;
                vehicle.CreatedOn = DateTime.Now;
                vehicle.UpdatedBy = User.Identity.Name;
                vehicle.UpdatedOn = DateTime.Now;
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "Name", vehicle.DriverId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleId,DriverId,RegNo,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "Name", vehicle.DriverId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
