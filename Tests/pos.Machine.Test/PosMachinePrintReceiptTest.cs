using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using pos.Machine;
using Xunit;

namespace pos.Machine.Test
{
    public class PosMachinePrintReceiptTest
    {
        [Fact]
        public void should_get_groupBarcode()
        {
            var posMachine = new PosMachine();
            var groupBarcode = posMachine.GroupBarcode(new[] {"001", "002", "001"}).ToArray();
            var resultCount = groupBarcode.Single(item => item.Barcode == "001");
            var result = groupBarcode.Single(item => item.Count == 1);
            Assert.Equal(2, resultCount.Count);
            Assert.Equal("Coca Cola", result.Barcode);
        }

        [Fact]
        public void should_get_products()
        {
            var posMachine = new PosMachine();
            var groupProduct = posMachine.groupProducts(new[] {new BarcodeStat("001", 2), new BarcodeStat("002", 3)});
            var firstProduct = groupProduct.Single(item => item.Barcode == "001");
            var secondProduct = groupProduct.First(item => item.Barcode == "002");
            Assert.Equal(2, firstProduct.Count);
            Assert.Equal(3, secondProduct.Price);
        }

        [Fact]
        public void should_get_ConvertToReceipt()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 2, Price: 3, Total: 6\n" +
                      "Name: Sprite, Amount: 2, Price: 3, Total: 6\n" +
                      " ---------------\n" +
                      " Total: 12";
            var posMachine = new PosMachine();
            string receipt = posMachine.ConvertToReceipt(
                new[] {new ProductStat("001", 2, "Coca Cola", 3), new ProductStat("002", 2, "Sprite", 3)});
            Assert.Equal(str, receipt);
        }


        [Fact]
        public void should_get_Receipt()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 2, Price: 3, Total: 6\n" +
                      "Name: Sprite, Amount: 1, Price: 3, Total: 3\n" +
                      " ---------------\n" +
                      " Total: 9";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"001","002","001"});
            Assert.Equal(str, receipt);
        }

        [Fact]
        public void should_return_empty_when_input_empty()
        {
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            var emptyParam = Array.Empty<string>();
//            Assert.Equal("", posMachine.GetReceipt(emptyParam));
            Assert.Throws(typeof(System.ArgumentNullException), () => posMachine.GetReceipt(emptyParam));
        }

        [Fact]
        public void should_return_str_when_input_one()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 1, Price: 3, Total: 3\n" +
                      " ---------------\n" +
                      " Total: 3";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"001"});
            Assert.Equal(str, receipt);
        }


        [Fact]
        public void should_return_str_when_input_more()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 1, Price: 3, Total: 3\n" +
                      "Name: Sprite, Amount: 1, Price: 3, Total: 3\n" +
                      " ---------------\n" +
                      " Total: 6";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"001", "002"});
            Assert.Equal(str, receipt);
        }

        [Fact]
        public void should_return_str_when_input_more_and_repeatOne()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 2, Price: 3, Total: 6\n" +
                      "Name: Sprite, Amount: 1, Price: 3, Total: 3\n" +
                      " ---------------\n" +
                      " Total: 9";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"001", "002", "001"});
            Assert.Equal(str, receipt);
        }

        [Fact]
        public void should_return_str_when_input_more_and_repeatMore()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 2, Price: 3, Total: 6\n" +
                      "Name: Sprite, Amount: 2, Price: 3, Total: 6\n" +
                      " ---------------\n" +
                      " Total: 12";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"001", "002", "001", "001"});
            Console.WriteLine(receipt);
            Assert.Equal(str, receipt);
        }


        [Fact]
        public void should_return_str_when_input_err_format()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 1, Price: 3, Total: 3\n" +
                      " ---------------\n" +
                      " Total: 3";
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            Assert.Throws(
                typeof(InvalidOperationException),
                () => posMachine.GetReceipt(new[] {"001", "01"}));
        }
    }
}