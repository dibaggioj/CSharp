using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Types
{
    /* inheritance and casting */

    public class y
    {
        public void x()
        {
            var animal = new Animal();
            var dog = new Dog();
            var poodle = new Poodle();

            TakeAnimal(animal); //this method only takes an Animal, so it can actually take dog too since a Dog is an Animal (it's supported through the inheritance structure)
            //TakeAnimal(dog);
            //TakeAnimal(poodle);
        }
        public void TakeAnimal(Animal a)
        {
            a.Temp = 98;

            if (a is Dog) // if 'a' is of type Dog
            {
                //var dog = (Dog)a; // casting syntax — this is safe here, because if we pass in just an animal, it will not cast, but it will raise an exception, so it's not safe for the application
                var dog = a as Dog; // casting syntax — safer
                if (dog != null) // becasue if 'a' is not a Dog, then var dog would be null (and null could still be type)
                    dog.Name = "Daren"; 
            }

        }
  
    }

    public class Animal { public int Temp { get; set; } }
    public class Dog : Animal { public string Name { get; set; } }
    public class Poodle : Dog { public string Groomer { get; set; } }

}
