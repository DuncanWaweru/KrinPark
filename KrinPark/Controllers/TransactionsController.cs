using KrinPark.Models;
using KrinPark.Repo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KrinPark.Controllers
{
    [RoutePrefix("api/{controller}/{action}")]
    public class TransactionsController : ApiController
    {
       
       // [HttpPost]
        public IHttpActionResult Response([FromBody] JToken result,string bookingId)
        {
            try
            {
                //var path = HttpContext.Current.Server.MapPath("/Uploads/logs.txt");

                //System.IO.File.WriteAllText(path, result.ToString());
                var stkresult = result.ToString();
                var response = JsonConvert.DeserializeObject<STKResponse>(stkresult);
                var _dataResponse = response.Body.stkCallback.CallbackMetadata.Item.ToList();
                var context = new ApplicationDbContext();
                PaymentResponse _Payments = new PaymentResponse();

                _Payments.Amount = Convert.ToInt32(_dataResponse[0].Value);
                _Payments.AccountNo = _dataResponse[1].Value.ToString();
                _Payments.CreatedOn = DateTime.Now;
                _Payments.PhoneNumber = _dataResponse[4].Value.ToString();
                _Payments.Paybill = "174379";
                context.PaymentResponse.Add(_Payments);
                context.SaveChanges();
                context.Payments.Add(new Payment() { Amount = _Payments.Amount, BookingId = Guid.Parse(bookingId), CreatedBy = User.Identity.Name, UpdatedBy = User.Identity.Name, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
              //  db.SaveChanges();
                context.SaveChanges();
                return Ok();
            }
            catch (Exception es)
            {
                var path = HttpContext.Current.Server.MapPath("/Uploads/logs.txt");

                System.IO.File.WriteAllText(path, es.ToString());
                return Ok();
            }
        }
    }
}
