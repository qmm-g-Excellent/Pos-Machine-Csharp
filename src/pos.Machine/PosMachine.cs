using System;
using System.Collections.Generic;
using Autofac;

namespace pos.Machine
{
    public interface IPosMachine
    {
         string GetReceipt(string[] items);
    }

    public class PosMachine : IPosMachine
    {
        public string GetReceipt(string[] items)
        {
            if (items.Length < 1)
                return "";           
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string item in items)
            {
                if (item.Length < 10)
                {
                    throw new FormatException();
                }
                int count = this.getRepeatItem(item, items);
                if (!dict.ContainsKey(item))
                {
                    dict.Add(item, count);
                }
            }

            string receipt = "";
            long total = 0;
            foreach (KeyValuePair<string, int> item in dict)
            {
                var productInformation = new ProductRepository().Get(item.Key.ToString());
                long subTotal = item.Value * productInformation.Price;
                total += subTotal;
                receipt += "Name: " + productInformation.Name + ", Amount: " + item.Value + ", Price: " +
                           productInformation.Price + ", Total: " + subTotal + "\n";
            }
            return "*** Receipt ***\n" + receipt +
                   " ---------------\n" +
                   " Total: " + total;
        }

        public int getRepeatItem(string barcode, string[] items)
        {
            int count = 0;
            foreach (string item in items)
            {
                if (barcode == item)
                     count++;
            }
            return count;
        }
    }
}