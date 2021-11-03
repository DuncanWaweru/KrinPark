using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class Driver : AuditTrail
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string IdNo { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}