using System;
using Autofac;
using pos.Machine;
using Xunit;

namespace pos.Machine.Test
{
    public class PosMachinePrintReceiptTest
    {     
        [Fact]
        public void should_return_empty_when_input_empty()
        {           
            IContainer container = PosMachineProgram.CreateContainer();
            var posMachine = container.Resolve<IPosMachine>();
            var emptyParam = Array.Empty<string>();
//            Assert.Equal("", posMachine.GetReceipt(emptyParam));
          Assert.Throws(typeof(System.ArgumentNullException), ()=> posMachine.GetReceipt(emptyParam));
        }

        [Fact]
        public void should_return_str_when_input_one()
        {
            var str = "*** Receipt ***\n" +
                      "Name: Coca Cola, Amount: 1, Price: 3, Total: 3\n" +                    
                      " ---------------\n" +
                      " Total: 3";
            IContainer container = PosMachineProgram.CreateContainer();
            var  posMachine = container.Resolve<IPosMachine>();
            string receipt = posMachine.GetReceipt(new[] {"ITEM000000"});
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
            string receipt = posMachine.GetReceipt(new[] { "ITEM000000","ITEM000001" });     
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
            string receipt = posMachine.GetReceipt(new[] { "ITEM000000", "ITEM000000", "ITEM000001" });
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
            string receipt = posMachine.GetReceipt(new[] { "ITEM000000", "ITEM000000", "ITEM000001", "ITEM000001" });
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
            Assert.Throws(typeof(FormatException),()=> posMachine.GetReceipt(new[] { "ITEM000000","ITEM000" }));
        }

    }
}