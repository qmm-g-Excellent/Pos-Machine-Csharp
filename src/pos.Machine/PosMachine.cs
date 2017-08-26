using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace pos.Machine
{
    public interface IPosMachine
    {
        string GetReceipt(string[] items);
    }


    public class BarcodeStat
    {
        public string Barcode;
        public int Count;

        public BarcodeStat(string barcode, int count)
        {
            Barcode = barcode;
            Count = count;
        }
    }

    public class ProductStat
    {
        public string Barcode;
        public int Count;
        public int Price;
        public string Name;

        public ProductStat(string barcode, int count, string name, int price)
        {
            Barcode = barcode;
            Count = count;
            Name = name;
            Price = price;
        }
    }

    public class PosMachine : IPosMachine
    {
        public string GetReceipt(string[] items)
        {
            IEnumerable<BarcodeStat> groupBarcode = this.GroupBarcode(items);
            IEnumerable<ProductStat> productList = this.groupProducts(groupBarcode);
            var receipt = this.ConvertToReceipt(productList);
            return receipt;
        }

        public IEnumerable<BarcodeStat> GroupBarcode(string[] items)
        {
            if (items.Length < 1 || items == null)
                throw new System.ArgumentNullException();
            IEnumerable<BarcodeStat> groupBarcod = items.GroupBy(item => item)
                .Select(item => new BarcodeStat(item.Key, item.Count()));
            return groupBarcod;
        }


        public IEnumerable<ProductStat> groupProducts(IEnumerable<BarcodeStat> groupBarcode)
        {
            IEnumerable<ProductStat> groupProduct = groupBarcode.Select(
                item =>
                {
                    var productInformation = new ProductRepositroy().Get(item.Barcode);
                    if (productInformation == null)
                        throw new InvalidOperationException();
                    return new ProductStat(item.Barcode, item.Count, productInformation.Name, productInformation.Price);
                });
            return groupProduct;
        }

        public string ConvertToReceipt(IEnumerable<ProductStat> product)
        {
            long total = 0;
            return product.Aggregate(
                       "*** Receipt ***\n",
                       (subReceipt, item) =>
                       {
                           var subtotal = item.Price * item.Count;
                           total += subtotal;
                           return subReceipt += "Name: " + item.Name + ", Amount: " + item.Count + ", Price: " +
                                                item.Price + ", Total: " + subtotal + "\n";
                       }) + " ---------------\n" +
                   " Total: " + total;
        }
    }
}

