using KrinPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Repo
{
    public class CheckPayments
    {
        public static List<Payment> Get(Guid bookingId)
        {
            var context = new ApplicationDbContext();
            return context.Payments.Where(x => x.BookingId == bookingId).ToList();
        }
        public static decimal GetAmountPaid(Guid bookingId)
        {
            var context = new ApplicationDbContext();
            var bookings = context.Payments.Where(x => x.BookingId == bookingId).ToList();
            if (bookings.Count < 1)
                return 0;
            return context.Payments.Where(x => x.BookingId == bookingId).Sum(x=>x.Amount);
        }
    }
}