using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class AuditTrail
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}