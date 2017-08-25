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
            var groupItems = items.GroupBy(item => item);
            long total = 0;
            return groupItems.Aggregate(
                       "*** Receipt ***\n",
                       (subReceipt, item) =>
                       {
                           var productInformation = new ProductRepository().Get(item.Key.ToString());
                           long subTotal = item.Count() * productInformation.Price;
                           total += subTotal;
                           return subReceipt +=
                               "Name: " + productInformation.Name + ", Amount: " + item.Count() + ", Price: " +
                               productInformation.Price + ", Total: " + subTotal + "\n";
                       }) + " ---------------\n" +
                   " Total: " + total;
        }


        public void WillThrowsException(string[] items)
        {
            if (items.Length < 1)
                throw new ArgumentNullException();
        }
    }
}