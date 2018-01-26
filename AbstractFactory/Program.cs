using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var abstractFactory = AbstractFactory.GetFactory(Architecture.Ember);
            var cpu = abstractFactory.CreateCpu();
        }
    }
}