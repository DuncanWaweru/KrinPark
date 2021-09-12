using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KrinPark.Models;

namespace KrinPark.Controllers
{
    public class ParkingRatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ParkingRates
        public ActionResult Index()
        {
            return View(db.ParkingRates.ToList());
        }

        // GET: ParkingRates/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingRate parkingRate = db.ParkingRates.Find(id);
            if (parkingRate == null)
            {
                return HttpNotFound();
            }
            return View(parkingRate);
        }

        // GET: ParkingRates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkingRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkingrateId,Amount")] ParkingRate parkingRate)
        {
            if (ModelState.IsValid)
            {
                parkingRate.ParkingrateId = Guid.NewGuid();
                parkingRate.CreatedBy = User.Identity.Name;
                parkingRate.CreatedOn = DateTime.Now;
                parkingRate.UpdatedBy = User.Identity.Name;
                parkingRate.UpdatedOn = DateTime.Now;
                db.ParkingRates.Add(parkingRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkingRate);
        }

        // GET: ParkingRates/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingRate parkingRate = db.ParkingRates.Find(id);
            if (parkingRate == null)
            {
                return HttpNotFound();
            }
            return View(parkingRate);
        }

        // POST: ParkingRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParkingrateId,Amount,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ParkingRate parkingRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkingRate);
        }

        // GET: ParkingRates/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingRate parkingRate = db.ParkingRates.Find(id);
            if (parkingRate == null)
            {
                return HttpNotFound();
            }
            return View(parkingRate);
        }

        // POST: ParkingRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ParkingRate parkingRate = db.ParkingRates.Find(id);
            db.ParkingRates.Remove(parkingRate);
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
