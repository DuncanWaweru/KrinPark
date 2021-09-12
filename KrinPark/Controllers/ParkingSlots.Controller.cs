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
    public class ParkingSlots : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ParkingLots
        public ActionResult Index()
        {
            return View(db.ParkingLots.ToList());
        }

        // GET: ParkingLots/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            return View(parkingLot);
        }

        // GET: ParkingLots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkingLots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkingLotSerialNo")] ParkingLot parkingLot)
        {
            int startNo = 1; 
            while (startNo<= Convert.ToInt32(parkingLot.ParkingLotSerialNo))
            {
                ParkingLot lot = new ParkingLot()
                {
                    ParkingLotId = Guid.NewGuid(),
                    ParkingLotSerialNo = "Slot - " + startNo,
                    ParkingLotStatus=  false,
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    UpdatedOn = DateTime.Now
                };
                db.ParkingLots.Add(lot);
                db.SaveChanges();
                startNo++;
            }
            return View(parkingLot);
        }

        // GET: ParkingLots/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            return View(parkingLot);
        }

        // POST: ParkingLots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParkingLotId,ParkingLotSerialNo,ParkingLotStatus")] ParkingLot parkingLot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingLot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkingLot);
        }

        // GET: ParkingLots/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            return View(parkingLot);
        }

        // POST: ParkingLots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            db.ParkingLots.Remove(parkingLot);
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
