using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class Class1
    {
        public string Name { get; set; }
    }
    public class Class2 : Class1
    {
        public int Age { get; set; }
    }
    public class Class3 : Class2
    {
        public string Address { get; set; }
    }
}