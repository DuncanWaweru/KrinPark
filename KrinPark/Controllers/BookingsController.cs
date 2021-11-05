using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KrinPark.Models;
using KrinPark.Repo;
using Microsoft.AspNet.Identity;

namespace KrinPark.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        [HttpGet]
        public ActionResult Index(string vehicleId,string plateNo)
        {
            ViewData["vehicleId"] = vehicleId;
            ViewData["plateNo"] = plateNo;
            //  var bookings = db.Bookings.Include(b => b.Vehicle);
         return   GetAvailableSlots(DateTime.Now.AddHours(1), DateTime.Now.AddHours(3),vehicleId);
        }
        [HttpPost]
        public ActionResult BookSlots(string slotId,string vehicleId, DateTime checkIn, DateTime checkOut)
        {
            var hours = checkOut - checkIn;
            var totalHours = Math.Ceiling(hours.TotalMinutes/60);
           return Create(new Booking() {

                CheckIn = checkIn,
                CheckOut = checkOut,
                ParkingFee = (decimal) totalHours * 1, //TODO: to set another figure this is for purposes of mpesa testing.
                CreatedBy = User.Identity.Name,
                CreatedOn = DateTime.Now,
                IsCancelled = false,
                ParkingLotId = Guid.Parse(slotId),
                UpdatedBy = User.Identity.Name,
                UpdatedOn = DateTime.Now,
                VehicleId = Guid.Parse(vehicleId)

            }); ;

            //var parkinglot
          //  return View();
        }

        [HttpPost]
        public ActionResult PayParkingFee(string bookingId,decimal amount)
        {
            var userName = User.Identity.GetUserId();
            var user = db.Users.Where(x=>x.Id== userName).SingleOrDefault();
            ///TODO show stk Push requesting payment;
            SafApiClass safApiClass = new SafApiClass();
            safApiClass.PhoneNumber = user.PhoneNumber;
            safApiClass.Amount = (int)amount;
            safApiClass.AccountNo = bookingId;
            safApiClass.MpesaStkPush();

           
            return RedirectToAction("GetMyBookings");
        }


        [HttpPost]
        public ActionResult GetAvailableSlots(DateTime checkIn, DateTime checkOut,string vehicleId)
        {
            ViewData["checkIn"] = checkIn;
            ViewData["checkOut"] = checkOut;
            ViewData["vehicleId"] = vehicleId;
            //create logic to fetch available slots
            //bookings where checkin not between mycheck in and mycheckout  && checkout not between  mycheck in and mycheckout 
            var bookings = db.Bookings.Include(x=>x.ParkingLot).Where(x=>(x.CheckIn>=checkIn|| x.CheckIn<=checkOut)&&(x.CheckOut>=checkIn|| x.CheckOut<=checkOut)).Select(x=>x.ParkingLot);
            //get all Parking lots
            var parkinglots = db.ParkingLots.ToList();
            //get the available parking lots 
            var availableLots=   parkinglots.Except(bookings);
            return View("Index",availableLots.ToList());
        }

        [HttpGet]
        public ActionResult GetMyBookings()
        {
            var userName = User.Identity.Name;
           
            return View(db.Bookings.Include(x=>x.ParkingLot).Where(x => x.CreatedBy == userName).ToList());
        }

        // GET: Bookings/Details/5A specified Include path is not valid. The EntityType 'KrinPark.Models.Booking' does not declare a navigation property with the name 'Payments'.'

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
           // ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,VehicleId,CheckIn,CheckOut,ParkingLotId,IsCancelled,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingId = Guid.NewGuid();
                db.Bookings.Add(booking);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            //ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", booking.VehicleId);
            return GetMyBookings();
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
