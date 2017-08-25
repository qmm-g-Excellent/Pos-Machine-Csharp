namespace pos.Machine
{
    public interface IProductInformation
    {
        string Name { get; }
        string Barcode { get; }
        long Price { get; }
    }
 
    internal class ProductInformation : IProductInformation
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public long Price { get; set; }

        public ProductInformation(string barcode,string name, long price)
        {

            this.Barcode = barcode;
            this.Name = name;
            this.Price = price;
//            this.
//            switch (barcode)
//            {
//                case "ITEM000000":
//                    this.Name = "Coca Cola";
//                    this.Barcode = "ITEM000000";
//                    this.Price = 3;                
//                    break;
//                case "ITEM000001":
//                    this.Name = "Sprite";
//                    this.Barcode = "ITEM000001";
//                    this.Price = 3;
//                    break;
//                case "ITEM000004":
//                    this.Name = "Battery";
//                    this.Barcode = "ITEM000004";
//                    this.Price = 3;
//                    break;
//                default: throw new System.FormatException();
//            }
        }
    }
}