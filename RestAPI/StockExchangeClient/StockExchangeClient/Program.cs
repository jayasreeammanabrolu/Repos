using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(SimpleClientRequest));
            t1.Start();
            

            string result = "";
            Stock msft = new Stock("MSFT", 43.28);
            string jsonStr = JsonConvert.SerializeObject(msft);
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "= " + jsonStr;
                result = client.UploadString("http://localhost:49703/api/stock", "POST", data);
                Console.WriteLine("Data posted to browser from main thread");
            }

            Stock msftupdate = new Stock("MSFT", 53.28);
            string jsonStrupdate = JsonConvert.SerializeObject(msftupdate);
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "= " + jsonStrupdate;
                result = client.UploadString("http://localhost:49703/api/stock", "Put", data);
                Console.WriteLine("Data updated in browser from main thread");
            }

            using (var client = new WebClient())
            {
                WebRequest request = WebRequest.Create("http://localhost:49703/api/stock");
                request.Method = "DELETE";
                request.GetResponse();
                Console.WriteLine("Data deleted in browser from main thread");
            }
           // Console.ReadLine();

        }

        private static void SimpleClientRequest()
        {
            string result = "";
            Stock msft = new Stock("Google", 103.6);
            string jsonStr = JsonConvert.SerializeObject(msft);
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "= " + jsonStr;
                result = client.UploadString("http://localhost:49703/api/stock", "POST", data);
                Console.WriteLine("Data posted in browser from thread t1");
            }

            Stock googupdate = new Stock("Google", 110.5);
            string jsonStrupdate = JsonConvert.SerializeObject(googupdate);
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "= " + jsonStrupdate;
                result = client.UploadString("http://localhost:49703/api/stock", "Put", data);
                Console.WriteLine("Data updated in browser from thread t1");                
            }
            Thread.Sleep(300);
            
        }

    }
}
