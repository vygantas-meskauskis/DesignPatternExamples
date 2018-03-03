using System;

namespace CommandPattern
{
    /*
     Consiquences
     -Commands must be completely self contained
        The client doesnt pass in any arguments
     - Easy to add new commands
        Just add a new class (open/closed principle)
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
        }
    }
}
