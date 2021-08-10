using System;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            DiContainer container = new DiContainer();
            
            container.Register<IInterfaceA, ClassA>();
            container.Register<IInterfaceB, ClassB>();

            // Instance of class A will be returned to class B
            IInterfaceB classB = container.Resolve<IInterfaceB>();
            classB.DoB();

            Console.ReadKey();
        }
    }
}