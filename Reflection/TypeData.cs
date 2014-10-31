using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionExamples
{

    internal class Dog { internal int NumberOfLegs { get; set; } }
    class TypeData
    {
        // getting type data:
        void GetTypeData()
        {
            var Dog = new Dog { NumberOfLegs = 4 };

            // At compile time:
            Type t1 = typeof(Dog);

            // At runtime:
            Type t2 = Dog.GetType();

            // output: Dog
            Console.WriteLine(t2.Name);

            /* output: After002, Version=1.0.0.0,
                       Culture=neutral, PublicKeyToken=null */
            Console.WriteLine(t2.Assembly);
        }

        // creating an instance of a type
        void CreateInstanceOfaType()
        {
            var newDog = (Dog)Activator.CreateInstance(typeof(Dog)); // equal to a Dog instance by casting the result of the CreateInstance method with the type of Dog being passed-in, so that CreateInstance know how to create an instance. Calls a default constructor on Dog to return that instance

            var genericDog = Activator.CreateInstance<Dog>(); // nicer implementation without casting, by using generics, saying the that will be returned; we communicate directly to it

            // uses default constructor
            // with no defined parameters
            var dogConstructor = typeof(Dog).GetConstructors()[0];

            //var advancedDog = (Dog)dogConstructor.Invoke(null); // THIS PARAMETER SHOULD ACTUALLY BE AN EMPTY ARRAY OF OBJECTS INSTEAD OF NULL, SINCE WE MAY BE PASSING IN A LIST OF TYPES
            var advancedDog = (Dog)dogConstructor.Invoke(new object[] {});
            // or, any of these?
            //var advancedDog = (Dog)dogConstructor.Invoke(new Type[0]);
            //var advancedDog = (Dog)dogConstructor.Invoke(object[] parameters);
            //var advancedDog = (Dog)dogConstructor.Invoke(obj object, object[] parameters);  
            
        }

        // accessing a property:
        void Property()
        {
            var horse = new Animal() { Name = "Ed" };

            var type = horse.GetType();

            var property = type.GetProperty("Name");

            var value = property.GetValue(horse, null);
            // value == "Ed"
        }

        internal class Animal
        {
            internal string Name { get; set; }
            internal string Speak() { return "Hello"; }
        }

        //invoking a method:
        void Method()
        {
            var horse = new Animal();

            var type = horse.GetType();

            var method = type.GetMethod("Speak");

            var value = (string)method.Invoke(horse, null);
            // value == "Hello"
        }

        //internal class Animal
        //{
        //    internal string Speak() { return "Hello"; }
        //}

    }
}
