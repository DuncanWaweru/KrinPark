using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace KrinPark.Repo
{

    public class SafApiClass
    { 
    
        public string Authtoken { get; set; }
        public string errorlog { get; set; }
        public string PhoneNumber { get; set; }
        public int Amount { get; set; }
        public string AccountNo { get; set; }

        public string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("+254"))
                return phoneNumber.Replace("+", ""); ;

            phoneNumber = phoneNumber.Replace("+", "");
            if (phoneNumber.StartsWith("01") || phoneNumber.StartsWith("07"))
            {
                phoneNumber = $"+254{phoneNumber.Substring(1)}";
            }
            else if (phoneNumber.StartsWith("1") || phoneNumber.StartsWith("7"))
            {
                phoneNumber = $"+254{phoneNumber}";
            }

            return phoneNumber.Replace("+", ""); ;
        }

        public string getAuth()
        {
          //String a = "https://api.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";
            String a = "https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";


            String baseUrl = a;

            String app_key = "cxVOUu0HxA4pKtRPWKqKVFr1T7E3p4Ek";
            String app_secret = "HxeGwDKIEeWu36mA";

            byte[] auth = Encoding.UTF8.GetBytes(app_key + ":" + app_secret);
            String encoded = System.Convert.ToBase64String(auth);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";
            request.Headers.Add("cache-control", "no-cache");
            request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                // Console.WriteLine(readStream.ReadToEnd());
                Authtoken = readStream.ReadToEnd();
                var jArray = JObject.Parse(Authtoken);
                // Index the Array and select your CompanyID
                var obj = jArray["access_token"].Value<string>();
                response.Close();
                readStream.Close();
                return obj;
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(resp);
                Authtoken = "";
                return Authtoken;
            }
        }
        public void MpesaStkPush()
        {
           //string a = "https://api.safaricom.co.ke/mpesa/stkpush/v1/processrequest";
            string a = "https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest";
            string baseUrl = a;
            string token = getAuth();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

           
            request.Headers.Add("authorization", "Bearer " + token);
            request.ContentType = "application/json";
            request.Headers.Add("cache-control", "no-cache");
            request.KeepAlive = true;
            request.Method = "POST";


            mpesadetails _mpesadetails = new mpesadetails()
            {

                    BusinessShortCode = "174379",
                    Password = "MTc0Mzc5YmZiMjc5ZjlhYTliZGJjZjE1OGU5N2RkNzFhNDY3Y2QyZTBjODkzMDU5YjEwZjc4ZTZiNzJhZGExZWQyYzkxOTIwMTkwMTMwMTI1NDI1",
                    Timestamp = "20190130125425", //2018-09-24T12:02:57+03:00
                    TransactionType = "CustomerPayBillOnline",
                    Amount = Amount,
                      PartyA = FormatPhoneNumber(PhoneNumber),
                      PartyB = "174379",
                      PhoneNumber = FormatPhoneNumber(PhoneNumber),
                      CallBackURL = "https://krinpark.solution.co.ke/api/Transactions/Response?bookingId=" + AccountNo, // "https://apiresponse.solution.co.ke/api/TestResponse/BalanceResult",
                AccountReference = "Test Account",
                      TransactionDesc = "Access search items"
            };

           // DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(mpesadetails));



            string json= JsonConvert.SerializeObject(
                _mpesadetails,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
          
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Console.WriteLine(readStream.ReadToEnd());
                response.Close();
                readStream.Close();
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(resp);
            }

        }
    }

    public class mpesadetails
    {
        public string BusinessShortCode { get; set; }
        public string Password { get; set; }
        public string Timestamp { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public string PartyA { get; set; }
        public string PartyB { get; set; }
        public string PhoneNumber { get; set; }
        public string CallBackURL { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDesc { get; set; }
    }
    public class MpesaJson
    {
        [JsonProperty("customer")]
        public PaymentDone payment { get; set; }

    }
    public class PaymentDone
    {
        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [JsonProperty("MpesaReceiptNumber")]
        public string MpesaReceiptNumber { get; set; }

      
    }
}