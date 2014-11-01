using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WorkingWithLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Enumerable.Range(1, 50); // helper that returns a list of numbers to us; Enumerable (namespace) and Range (static method)

            var method = // IEnumerable<string>
                 data.Where(x => x % 2 == 0) // Predicate returns true or false, and here we pass a value x into the expression that is being evaluated, where x is the current item from data. The value of x is projected into the predicate, which then returns to true or false (in this case, if the number is odd or even)
                 .Select(x => x.ToString()); // .Select is a projection tool. Here we take the input, x (an integer), and transorm it into a string, using the lambda statement. (Note: project means to take data and project it into a new type or structure, i.e. change the data shape)
                        // instead of returning a string above, you could also return an anonymous type or a fully defined type instead if you'd want
            // above is the definition of the LINQ action, not the execution of the action, so you can append other things to it (like additional Wheres, etc). We have here a queryable object, and each extension method is basically implementing queryable and returns a queryable, which allows you to chain together mulitple methods

            var query = // IEnumerable<string>
                from d in data
                where d % 2 == 0
                select d.ToString();

            // the results that go into method and the results that go into query are be exactly the same; one just uses the method syntax of LINQ, the other uses the query syntax of LINQ

            Debugger.Break(); // a hard-coded breakpoint

            var projection =
                from d in data
                select new // we're going to return 3 properties
                {
                    Even = (d % 2 == 0), 
                    Odd = !(d % 2 == 0),
                    Value = d,
                };
            //properties becomes a fully fledged object, which has three properties: Even, Odd, and Value

            var letters = new[] { "A", "C", "B", "E", "Q" };

            Debugger.Break();

            var sortAsc =
                from d in letters
                orderby d ascending
                select d;

            var sortDesc =
                letters.OrderByDescending(x => x);

            Debugger.Break();

            // candy

            var values = new[] { "A", "B", "A", "C", "A", "D" };

            // these helper methods do what you could've done manually
            var distinct = values.Distinct(); // removes duplicates
            var first = values.First(); // 
            var firstOr = values.FirstOrDefault();
            var last = values.Last();
            var page = values.Skip(2).Take(2);

            Debugger.Break();

            // aggregates

            var numbers = Enumerable.Range(1, 50);
            var any = numbers.Any(x => x % 2 == 0);
            var count = numbers.Count(x => x % 2 == 0);
            var sum = numbers.Sum();
            var max = numbers.Max();
            var min = numbers.Min();
            var avg = numbers.Average();

            Debugger.Break();

            var dictionary = new Dictionary<string, string>()
            {
                 {"1", "B"}, {"2", "A"}, {"3", "B"}, {"4", "A"},
            };

            var group = // IEnumerable<string, IEnumerable<string>>
                from d1 in dictionary
                group d1 by d1.Value into g
                select new
                {
                    Key = g.Key,
                    Members = g,
                };

            Debugger.Break();

            var dictionary1 = new Dictionary<string, string>()
            {
                 {"1", "B"}, {"2", "A"}, {"3", "B"}, {"4", "A"},
            };

            var dictionary2 = new Dictionary<string, string>()
            {
                 {"5", "B"}, {"6", "A"}, {"7", "B"}, {"8", "A"},
            };

            var join =
                from d1 in dictionary1
                join d2 in dictionary2 on d1.Value equals d2.Value
                select new
                {
                    Key1 = d1.Key,
                    Key2 = d2.Key,
                    Value = d1.Value
                };

            Debugger.Break();

        }
    }
}
