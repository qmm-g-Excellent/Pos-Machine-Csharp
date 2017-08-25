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
            if (items.Length < 1 || items == null)
                throw new ArgumentNullException();
//            var incorrectFormat = items.Where(item => item.Length)
            var groupItems = items.GroupBy(item => item);
            long total = 0;
            return groupItems.Aggregate(
                       "*** Receipt ***\n",
                       (subReceipt, item) =>
                       {
                           var productInformation = new ProductRepository().Get(item.Key.ToString());
                           if (productInformation == null)
                               throw new InvalidOperationException();
                           long subTotal = item.Count() * productInformation.Price;
                           total += subTotal;
                           return subReceipt +=
                               "Name: " + productInformation.Name + ", Amount: " + item.Count() + ", Price: " +
                               productInformation.Price + ", Total: " + subTotal + "\n";
                       }) + " ---------------\n" +
                   " Total: " + total;
        }       
    }
}