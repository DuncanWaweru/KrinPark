using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class Booking : AuditTrail
    {
        public Guid BookingId { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Guid ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsCancelled { get; set; }
        public decimal ParkingFee { get; set; }
        public  IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}