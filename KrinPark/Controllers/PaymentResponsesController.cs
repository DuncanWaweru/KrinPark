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
    public class PaymentResponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentResponses
        public ActionResult Index()
        {
            return View(db.PaymentResponse.ToList());
        }

        // GET: PaymentResponses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentResponse paymentResponse = db.PaymentResponse.Find(id);
            if (paymentResponse == null)
            {
                return HttpNotFound();
            }
            return View(paymentResponse);
        }

        // GET: PaymentResponses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount,Paybill,AccountNo,PhoneNumber,CreatedOn,Redeemed")] PaymentResponse paymentResponse)
        {
            if (ModelState.IsValid)
            {
                paymentResponse.Id = Guid.NewGuid();
                db.PaymentResponse.Add(paymentResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentResponse);
        }

        // GET: PaymentResponses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentResponse paymentResponse = db.PaymentResponse.Find(id);
            if (paymentResponse == null)
            {
                return HttpNotFound();
            }
            return View(paymentResponse);
        }

        // POST: PaymentResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,Paybill,AccountNo,PhoneNumber,CreatedOn,Redeemed")] PaymentResponse paymentResponse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentResponse);
        }

        // GET: PaymentResponses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentResponse paymentResponse = db.PaymentResponse.Find(id);
            if (paymentResponse == null)
            {
                return HttpNotFound();
            }
            return View(paymentResponse);
        }

        // POST: PaymentResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PaymentResponse paymentResponse = db.PaymentResponse.Find(id);
            db.PaymentResponse.Remove(paymentResponse);
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
