using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlFlow
{
    class Program
    {
        enum Dogs {  Lassie, Snoopy, Yeller } // enum creates strongly-typed options

        static void Main(string[] args)
        {
            /* for, single variable */
            //for (int i = 0; i < 10; i++)
            //{
            //    //do something
            //}

            /* for,  multiple variables */
            //for (int i = 0, j = 0, k = 10; i < 10; i++, j++, k+=10)
            //{
            //    //do something
            //    Console.WriteLine("i = {0}, j = {1}, k = {2}", i, j, k);
            //}
            //Console.Read(); 

            /* for each */
            //var list = new[] { 1, 2, 3, 4, 5, 6, 7 }; // shorthand for initiating a list, and 'var' keyword lets the compiler determine which type 'list' needs to be
            //foreach (var item in list) // 'var' keyword lets compiler determine which type 'item' needs to be
            //{
            //    if (item % 2 == 0) // if item is an even value
            //        break;
            //    // TODO this
            //}
            //// TODO more!

            /* switch */
            //var snoopy = Dogs.Snoopy;
            //switch (snoopy)
            //{
            //    case Dogs.Lassie: //removing the break from below Lassie would allow Lassie to fall into Snoopy, because although Lassie hasn't been implemented Snoopy has been implemented
            //        Console.WriteLine("Hi");
            //        break;
            //    case Dogs.Snoopy:
            //        Console.WriteLine("Hi");
            //        break;
            //    case Dogs.Yeller:
            //        break;
            //    default: // optional
            //        throw new NotSupportedException(); // not necessary but this is a good way to communicate to the developer that something isn't covered. Also, throw then takes the palce of break, since it stops code execution
            //}
            
        }
    }
}
