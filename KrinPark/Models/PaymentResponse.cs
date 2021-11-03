using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrinPark.Models
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "PayBill No.")]
        public string Paybill { get; set; }
        [Required]
        [Display(Name = "Account No.")]
        public string AccountNo { get; set; }
        [Required]
        [Display(Name = "Phone Number No.")]
        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }
        [DefaultValue(false)]
        public Boolean Redeemed { get; set; }
    }
}