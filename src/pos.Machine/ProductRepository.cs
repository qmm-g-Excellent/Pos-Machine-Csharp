using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace pos.Machine
{
    public interface IProductRepository
    {
        IProductInformation Get(string barcode);
    }

    internal class ProductRepository : IProductRepository
    {
        readonly List<ProductInformation> productRep = new List<ProductInformation>();

        public ProductRepository()
        {
            productRep.Add(new ProductInformation("ITEM000000", "Coca Cola", 3));
            productRep.Add(new ProductInformation("ITEM000001", "Sprite", 3));
            productRep.Add(new ProductInformation("ITEM000002", "Battery", 3));
        }

        public IProductInformation Get(string barcode)
        {
            return productRep.First(item => item.Barcode == barcode) ?? null;
        }
    }
}