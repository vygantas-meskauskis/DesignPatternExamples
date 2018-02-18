using System;

namespace Adapter
{
    /*
     - A class that would be useful to your application does not implement the interface you require
     - You are designing a class or a framework and you want to nesure
     it is usable by a wide variety of as-yet-unwritten classes and applications
     -Adapters are also commonly known as Wrappers

    - Convert the interface of a class into another interface clients expect
    - Allow classes to work together that couldnt otherwise due to incompatible interfaces
    - Future-proof client implementations by having them depend on Adapter interface, rather
    than concrete classes dirrectly.

    How it gets used:
    - Client depend on the Adapter interface rather than a particural implementation
    - At least one concrete Adapter class is created to allow the client to work with
    a particural class that it requires
    - Future client needs for alternate implementations can be satisfied through the creation of additional
    concrete Adapter classes
    - Efective way to achieve Open/Closed Principle

    Related Patterns:
    - Repository
    - Strategy (the adapter pattern is often passed into a class that depends on it, thus implementing
    the strategy pattern)
    - Facade. Addapter and Facade are both wrappers. The Facade pattern attemplts to simplify the
    interface and often wraps many classes, while Adapter typically wraps a single. Adaptee, and is not generaly
    concerned with simplifying the interface.
     */
    class Program
    {
        static void Main(string[] args)
        {
            IAdapter adapter = null;

            var num = 1;

            switch (num)
            {
                case 1:
                    adapter = new AdapterOne();
                    break;
                case 2:
                    adapter = new AdapterTwo();
                    break;
            }

            adapter.Do();
        }
    }

    class LibraryOne
    {
        public void ThisIsHowItDoesIt()
        {
            Console.WriteLine("Library one");
        }
    }

    class LibraryTwo
    {
        public void ThisIsHowItDoesIt()
        {
            Console.WriteLine("Library Two");
        }
    }

    interface IAdapter
    {
        void Do();
    }

    class AdapterOne : IAdapter
    {
        private LibraryOne _library;

        public AdapterOne()
        {
            _library = new LibraryOne();
        }

        public void Do()
        {
            _library.ThisIsHowItDoesIt();
        }
    }

    class AdapterTwo :IAdapter
    {
        private LibraryTwo _library;

        public AdapterTwo()
        {
            _library = new LibraryTwo();
        }

        public void Do()
        {
            _library.ThisIsHowItDoesIt();
        }
    }
}
