using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeClient
{
    class Stock
    {
        public string stockName { get; set; }
        public double stockValue { get; set; }

        public Stock(string name, double value)
        {
            this.stockName = name;
            this.stockValue = value;
        }
    }
}
