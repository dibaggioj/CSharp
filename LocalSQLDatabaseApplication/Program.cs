using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSQLDatabaseApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Database1Entities();

            var products = context.Products; // context.Products will return the enumerable of the products. You can inject a predicate here using a Lambda statement ("=>"). In context.Products.Where, the Where (an extension method part of LINQ) clause is translated into T-SQL, allowing you to interact with the database as if you're typing SQL to it; this also provides compile-time checking. The ORM handles the T-SQL, allowing you to write statements like the following:

            Console.WriteLine("All products:");
            foreach (var product in products)
                Console.WriteLine("{0} : {1:C}", product.Name, product.Price);

            Console.WriteLine();

            //foreach (var product in products
            //    .Where(x => x.Name.Contains("3"))) // x is an argument (for product) being passed into the lambda ("=>") statement
            //{
            //    Console.WriteLine(product.Name); // this should only display Product3
            //}

            Console.WriteLine("Products under $100:");
            foreach (var product in products
                .Where(x => x.Price <= 100)) // x is an argument (for product) being passed into the lambda ("=>") statement
            {
                Console.WriteLine("{0} : {1:C}", product.Name, product.Price); // this should only display all products under $100
            }
            Console.WriteLine();

            var firstProd = products.First(); // .First() is another extension method provided by LINQ
            Console.WriteLine("First product is {0}", firstProd.Name);

            var firstProd2 = products.First(x => x.Price >= 100); // .First() extension method allows you to inject a predicate too
            Console.WriteLine("First product above $100 is {0}", firstProd2.Name); 

            var firstProd3 = products.First( x => x.Name.EndsWith("3")); // .First() extension method allows you to inject a predicate too
            Console.WriteLine("First product ending in '3' is {0}", firstProd3.Name); // should return Product3

            var cheapProd = products.First(x => x.Price <=20);
            Console.WriteLine("First product under $20 is {0}", cheapProd.Name); 

            // make change to product:
            cheapProd.Price += 20; //add $20 to that price
            Console.WriteLine("New price for {0} is {1:C}", cheapProd.Name, cheapProd.Price);
            
            context.SaveChanges(); // saves all changes currently pending within the context

            Console.WriteLine();
            Console.WriteLine("All products updated:");
            foreach (var product in products)
                Console.WriteLine("{0} : {1:C}", product.Name, product.Price);

            var firstItem = products.First( x => x.Name.StartsWith("Item"));
            context.Products.Remove(firstItem);

            context.SaveChanges(); // saves all changes currently pending within the context

            Console.WriteLine();
            Console.WriteLine("All products updated:");
            foreach (var product in products)
                Console.WriteLine("{0} : {1:C}", product.Name, product.Price);

            firstItem = products.First(x => x.Name.StartsWith("Item"));
            context.Products.Remove(firstItem);

            context.SaveChanges(); // saves all changes currently pending within the context

            Console.WriteLine();
            Console.WriteLine("All products updated:");
            foreach (var product in products)
                Console.WriteLine("{0} : {1:C}", product.Name, product.Price);

            Console.Read();

        }



    }

}
