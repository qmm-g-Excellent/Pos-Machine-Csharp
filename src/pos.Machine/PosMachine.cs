using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace pos.Machine
{
    public interface IPosMachine
    {
        string GetReceipt(string[] items);
    }

    internal class PosMachine : IPosMachine
    {
        public string GetReceipt(string[] items)
        {
            this.WillThrowsException(items);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            this.GetDictionary(dict, items);
            long total = 0;
            return  dict.Aggregate(
                                 "*** Receipt ***\n",
                (subReceipt, item) =>
                {
                    var productInformation = new ProductRepository().Get(item.Key.ToString());
                    long subTotal = item.Value * productInformation.Price;
                    total += subTotal;
                    return subReceipt += "Name: " + productInformation.Name + ", Amount: " + item.Value + ", Price: " +
                                         productInformation.Price + ", Total: " + subTotal + "\n";
                })+ " ---------------\n" +
                " Total: " + total;
//            return "*** Receipt ***\n" + receipt +
//                   " ---------------\n" +
//                   " Total: " + total;
        }

        public void GetDictionary(Dictionary<string, int> dict , string[] items)
        {
            foreach (string item in items)
            {
                if (!dict.ContainsKey(item))
                    dict.Add(item, 1);
                else
                    dict[item] += 1;
            }
        }

        public void WillThrowsException(string[] items)
        {
            if (items.Length < 1)
                throw new ArgumentNullException();
            else
            {
                string[] incorrectFormat = items.Where(ele => ele.Length < 10).ToArray();
                if (incorrectFormat.Length > 0)
                {
                    throw new FormatException();
                }
            }
        }
    }
}