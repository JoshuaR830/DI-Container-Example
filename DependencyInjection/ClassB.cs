using System;

namespace DependencyInjection
{
    public class ClassB : IInterfaceB
    {

        private IInterfaceA _classA;

        // Depends on InterfaceA which is injected in the container
        public ClassB(IInterfaceA classA)
        {
            this._classA = classA;
        }

        // Calling this should print 2 lines
        public void DoB()
        {
            _classA.DoA();
            Console.WriteLine("Do B");
        }
    }
}