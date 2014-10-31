using System;
using System.Reflection;

namespace CodeReflection
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // without reflection
            ////var dog = new Dog { NumberOfLegs = 4 };
            ////Console.WriteLine("A dog has {0} legs", dog.NumberOfLegs);

            // with reflection
            //object dog = Activator.CreateInstance(typeof(Dog));
            var dog = Activator.CreateInstance(typeof(Dog)) as Dog; // using a generic, this would cast dog to an object, so this would also work instead
            // dog.NumberOfLegs is available here
            //PropertyInfo[] properties = typeof(Dog).GetProperties();
            PropertyInfo[] properties = dog.GetType().GetProperties(); // returns an array with all properties; we only have one property so we'll have an array with one member
            PropertyInfo numberOfLegsProperty1 = properties[0]; // since we know we ahve just one property, we can specify the first index position in the properties array

            // or
            PropertyInfo numberOfLegsProperty2 = null;
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.Equals("NumberOfLegs", StringComparison.InvariantCulture)) // make sure NumberOfLegs is there and, if so, interact with it
                {
                    numberOfLegsProperty2 = propertyInfo;
                }
            }

            numberOfLegsProperty1.SetValue(dog, 3, null);

            Console.WriteLine(numberOfLegsProperty2.GetValue(dog, null));

            // use reflection to invoke different constructors

            
            var defaultConstructor = typeof(Dog).GetConstructor(new Type[0]); // this is the first constructor, which takes no parameteres
            var legConstructor = typeof(Dog).GetConstructor(new[] { typeof(int) }); // this is the second constructor, which takes an integer value. So we pass in an array the contains the int type. Here, GetConstructor looks for a constructor that matches that pattern. We're asking for a specific constructor (not all of the constructors), so we say what the signature is

            var defaultDog = (Dog)defaultConstructor.Invoke(null);
            Console.WriteLine(defaultDog.NumberOfLegs);

            var legDog = (Dog)legConstructor.Invoke(new object[] { 5 });
            Console.WriteLine(legDog.NumberOfLegs);

            Console.Read();
        }
    }

    internal class Dog
   { 
        public int NumberOfLegs { get; set; }

        public Dog() // default constructor
        {
            NumberOfLegs = 4;
        }

        public Dog(int legs) // manually call overload of its constructor, to set any number of legs you want
        {
            NumberOfLegs = legs;
        }
    }
}