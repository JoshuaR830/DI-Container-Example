using System;

namespace DependencyInjection
{
    public class ClassA : IInterfaceA
    {
        public void DoA()
        {
            Console.WriteLine("Do A");
        }
    }
}