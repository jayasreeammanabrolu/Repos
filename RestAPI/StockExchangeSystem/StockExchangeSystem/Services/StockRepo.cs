using StockExchangeSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockExchangeSystem.Services
{
    public class StockRepo
    {
        private static Dictionary<string, Stock> repoDictionary = null;
        private static object objlock = new object();

        public static void AddValues()
        {
            lock (objlock)
            {
                if (repoDictionary == null)
                    repoDictionary = new Dictionary<string, Stock>();

                if (!repoDictionary.ContainsKey("Amazon"))
                    repoDictionary.Add("Amazon", new Stock() { stockName = "Amazon", stockValue = 111.14 });

                if (!repoDictionary.ContainsKey("Facebook"))
                    repoDictionary.Add("Facebook", new Stock() { stockName = "Facebook", stockValue = 153.12 });
            }
            
        }


        public static List<Stock> GetAllStocks()
        {
            lock (objlock)
            {
                if (repoDictionary == null)
                    return null;
                List<Stock> list = new List<Stock>();
                for (int i = 0; i < repoDictionary.Count; i++)
                {
                    list.Add(repoDictionary.ElementAt(i).Value);
                }
                return list;

            }
            
        }

        public static Stock GetStock(string name)
        {
            lock (objlock)
            {
                if (repoDictionary == null)
                    return null;

                if (!repoDictionary.ContainsKey(name))
                    return null;

                return repoDictionary[name];
            }
            
        }

        public static string AddStock(string name, double value)
        {
            lock (objlock)
            {
                if (!repoDictionary.ContainsKey(name))
                {
                    Stock stock = new Stock() { stockName = name, stockValue = value };
                    repoDictionary.Add(name, stock);
                }
                return name;
            }

        }

        public static bool UpdateStock(string name,double value)
        {
            lock (objlock)
            {
                if (repoDictionary == null)
                    return false;
                if (!repoDictionary.ContainsKey(name))
                    return false;
                repoDictionary[name].stockValue = value;
                return true;
            }
            
        }

        public static bool DeleteStock(string s)
        {
            lock (objlock)
            {
                if (repoDictionary.ContainsKey(s))
                {
                    repoDictionary.Remove(s);
                    return true;
                }

                else
                {
                    return false;
                }
            }
         
        }
    }
}
