using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class Vehicle : AuditTrail
    {
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }
        public string RegNo { get; set; }
    }
}