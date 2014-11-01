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

            var products = context.Products; // context.Products will return the enumerable of the products. You can inject a predicate here using a Lambda statement ("=>"). In context.Products.Where, the Where (an extension method part of LINQ) clause is translated into T-SQL, allowing you to interact with the database as if you're typing SQL to it; this also provides compile-time checking
            
            //foreach (var product in products)
            //    Console.WriteLine(product.Name);

            //foreach (var product in products
            //    .Where(x => x.Name.Contains("3"))) // x is an argument (for product) being passed into the lambda ("=>") statement
            //{
            //    Console.WriteLine(product.Name); // this should only display Product3
            //}

            foreach (var product in products
                .Where(x => x.Price <= 100)) // x is an argument (for product) being passed into the lambda ("=>") statement
            {
                Console.WriteLine(product.Name); // this should only display all products under $100
            }
            Console.Read();

        }
    }
}
