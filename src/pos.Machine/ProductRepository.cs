using System.Collections.Generic;
using Autofac;

namespace pos.Machine
{
    public interface IProductRepository
    {
        IProductInformation Get(string barcode);
    }

    internal class ProductRepository : IProductRepository
    {
        public IProductInformation Get(string barcode)
        {
            var productInformation = new ProductInformation(barcode);
            return productInformation;
        }
    }
}