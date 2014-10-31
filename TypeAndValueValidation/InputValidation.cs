using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TypeAndValueValidation
{
    class InputValidation
    {
        static void Main(string[] args)
        {
            Animal cat = new Cat();
            Animal dog = new Dog();

            if (cat is Dog)
                throw new NotSupportedException("Dogs only!");

            if (cat == dog)
                throw new Exception("Not the same");

            if (cat.Equals(dog))
                throw new Exception("Not equal");
        }

        public abstract class Animal
        {
            public string Name { get; protected set; }
            public abstract void SetName(string value);
        }

        public class Cat : Animal
        {
            public override void SetName(string value)
            {
                // validate empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");

                // validate conflict
                if (value == this.Name)
                    throw new ArgumentException("value is duplicate");

                // validate size
                if (value.Length > 10)
                    throw new ArgumentException("value is too long");

                Contract.EndContractBlock(); // this will roll up everything above and treat these as contracts, but the use of predicates (below) is a more unified way of handling both input and output

                this.Name = value;
            }
        }

        // best practices — using contracts for data validation
        public class Dog : Animal
        {
            public string Name { get; protected set; }
            public override void SetName(string value)
            {
                // validate input
                Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty"); // use .Requires for preconditions — if the predicate is null or white space, then there's a message passed to the user
                // you can add multiple Contract.Requires to check everything you want to check
                // Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty");
                // Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty");
                this.Name = value;
            }

            public string GetName()
            {
                // validate output
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>())); // the predicate, Contract.Result<string>(), you can not return an empty stringw
                // you can add multiple Contract.Ensures to check everything you want to check
                // Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                // Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                return this.Name;
            }
        }

    }
}