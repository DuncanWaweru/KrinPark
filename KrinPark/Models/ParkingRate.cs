using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class ParkingRate :AuditTrail
    {
        public Guid ParkingrateId { get; set; }
        public decimal Amount { get; set; } //amount for each hour involved
    }
}