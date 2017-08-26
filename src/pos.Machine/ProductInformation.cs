namespace pos.Machine
{
    public interface IProductInformation
    {
        string Name { get; }
        string Barcode { get; }
        int Price { get; }
    }

    internal class ProductInformation : IProductInformation
    {
        public string Name { get; set;}
        public string Barcode { get; set; }
        public int Price { get; set; }
//        public int count { get; set; }

        public ProductInformation(string barcode, string name, int price)
        {
            this.Barcode = barcode;
            this.Name = name;
            this.Price = price;
        }
    }
}