using Newtonsoft.Json;
using StockExchangeSystem.Models;
using StockExchangeSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockExchangeSystem.Controllers
{
    public class StockController : ApiController
    {
         public StockController()
        {
            StockRepo.AddValues(); 
        }


        // GET api/contact
        public IEnumerable<Stock> Get()
        {
            return StockRepo.GetAllStocks();
        }

        // GET api/Contact/5
        public Stock Get(int id)
        {
            if(id==1)
            return StockRepo.GetStock("Amazon");
            else 
                return StockRepo.GetStock("Facebook");
             
        }

        public void Post([FromBody] string value)
        {
            Stock msft = JsonConvert.DeserializeObject<Stock>(value);
            StockRepo.AddStock(msft.stockName, msft.stockValue);           
        }

        public void Put([FromBody] string value)
        {
            Stock msftupdate = JsonConvert.DeserializeObject<Stock>(value);
            StockRepo.UpdateStock(msftupdate.stockName, msftupdate.stockValue);     
     
        }

       //  DELETE api/values/5
        public HttpResponseMessage Delete()
        {
            string s = "Google";
            if (StockRepo.DeleteStock(s))
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            
        }
    }
}
