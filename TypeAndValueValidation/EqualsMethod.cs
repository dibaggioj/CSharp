using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeAndValueValidation
{
    /* The following example shows how to override the Object.Equals(Object) method to test for value equality. 
     * It overrides the Equals method for the Person class. If Person accepted its base class implementation of 
     * equality, two Person objects would be equal only if they referenced a single object. However, in this case, 
     * two Person objects are equal if they have the same value for the Person.Id property. 
     * http://msdn.microsoft.com/en-us/library/bsc2ak47(v=vs.110).aspx
    */

    public class Person
    {
        private string idNumber;
        private string personName;

        public Person(string name, string id)
        {
            this.personName = name;
            this.idNumber = id;
        }

        public override bool Equals(Object obj)
        {
            Person personObj = obj as Person;
            if (personObj == null)
                return false;
            else
                return idNumber.Equals(personObj.idNumber);
        }

        public override int GetHashCode()
        {
            return this.idNumber.GetHashCode();
        }
    }

    public class Example
    {
        //public static void Main()
        public static void DoExample()
        {
            Person p1 = new Person("John", "63412895");
            Person p2 = new Person("Jack", "63412895");
            Console.WriteLine(p1.Equals(p2));
            Console.WriteLine(Object.Equals(p1, p2));
        }
    }
    // The example displays the following output: 
    //       True 
    //       True
}
