using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrinPark.Repo
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Item
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class CallbackMetadata
    {
        public List<Item> Item { get; set; }
    }

    public class StkCallback
    {
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public CallbackMetadata CallbackMetadata { get; set; }
    }

    public class Body
    {
        public StkCallback stkCallback { get; set; }
    }

    public class STKResponse
    {
        public Body Body { get; set; }
    }


}