using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class ParkingLot:AuditTrail
    {
    public Guid ParkingLotId { get; set;}
    public string ParkingLotSerialNo { get; set; }
    public bool ParkingLotStatus { get; set; }
    }
}