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
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Vehicle);
            return View(bookings.ToList());
        }
        [HttpPost]
        public ActionResult BookSlots(string slotId)
        {
            //var parkinglot
            return View();
        }

        [HttpPost]
        public ActionResult GetAvailableSlots(DateTime checkIn, DateTime checkOut)
        {
            //create logic to fetch available slots
            //bookings where checkin not between mycheck in and mycheckout  && checkout not between  mycheck in and mycheckout 
            var bookings = db.Bookings.Include(x=>x.ParkingLot).Where(x=>(x.CheckIn>=checkIn|| x.CheckIn<=checkOut)&&(x.CheckOut>=checkIn|| x.CheckOut<=checkOut)).Select(x=>x.ParkingLot);
            //get all Parking lots
            var parkinglots = db.ParkingLots.ToList();
            //get the available parking lots 
            var availableLots=   parkinglots.Except(bookings);
            return View("Index",availableLots.ToList());
        }


        // GET: Bookings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,VehicleId,CheckIn,CheckOut,IsCancelled,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingId = Guid.NewGuid();
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", booking.VehicleId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", booking.VehicleId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,VehicleId,CheckIn,CheckOut,IsCancelled,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", booking.VehicleId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
