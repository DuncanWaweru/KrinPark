using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class Payment : AuditTrail
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
    }
}