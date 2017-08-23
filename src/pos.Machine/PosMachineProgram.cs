using Autofac;

namespace pos.Machine
{
    public class PosMachineProgram
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new PosMachine()).As<IPosMachine>();
            IContainer container = builder.Build();
            return container;
        }
    }

}