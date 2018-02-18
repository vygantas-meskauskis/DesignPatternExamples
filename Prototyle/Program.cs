using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyle
{
    class Program
    {
        static void Main(string[] args)
        {
            var object1 = new SomeObject();

            var cloneObject = object1.Clone();
        }
    }

    public class SomeObject : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
