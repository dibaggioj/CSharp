using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeAndValueValidation
{
    public class DataValidationBase
    {
        public string Name { get; set; }
        public virtual void SetName(string value) 
        {
            // no protection here

            this.Name = value;
        }
    }
    
    //demo of a full scaffolding around protecting your data
    class DataValidation : DataValidationBase
    {
        public override void SetName(string value)
        {
            // validate empty (make sure it’s not null or empty)
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            // validate conflict (here, you can’t set name to itself)
            if (value == this.Name)
                throw new ArgumentException("value is duplicate");

            // validate size
            if (value.Length > 10)
                throw new ArgumentException("value is too long");

            this.Name = value;
        }
    }

    // "is" statement
    public class Pet 
    {
        private void Pets()
        {
            Pet cat = new PetCat();
            Pet dog = new PetDog();

            if (cat is PetDog) // check to see if cat is the PetDog type 
                throw new NotSupportedException("Dogs only!");

            if (cat == dog) // check to see if these are the exact same object in memory (this could be the case if we had two variables that equaled each other)
                throw new Exception("Not the same");

            if (cat.Equals(dog)) // check to see if they have the same values (not if they're the same object). You can override Equals to make sure it checks exactly what you need it to check (e.g., check Name only)
                throw new Exception("Not equal");
        }
    }
    class PetCat : Pet { }
    class PetDog : Pet { }
     

}
