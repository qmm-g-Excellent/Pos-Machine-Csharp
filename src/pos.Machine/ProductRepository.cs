using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace pos.Machine
{
    public interface IProductRepository
    {
        IProductInformation Get(string barcode);
    }

    internal class ProductRepositroy : IProductRepository
    {
        readonly List<ProductInformation> productRep = new List<ProductInformation>();
        public ProductRepositroy()
        {
            productRep.Add(new ProductInformation("001", "Coca Cola", 3));
            productRep.Add(new ProductInformation("002", "Sprite", 3));
            productRep.Add(new ProductInformation("003", "BAttery", 3));
        }

        public IProductInformation Get(string barcode)
        {
            return productRep.First(item => item.Barcode == barcode) ?? null;
        }
    }
}