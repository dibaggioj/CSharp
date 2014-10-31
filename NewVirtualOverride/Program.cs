using System;

namespace NewVirtualOverride
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var baseClass = new BaseClass();
            var derivedOverride = new DerivedOverride();
            var derivedNew = new DerivedNew();
            var derivedOverWrite = new DerivedOverwrite();

            // each of the following calls the Name method from its own class (there's no casting happening here)
            baseClass.Name(); // will display "BaseClass"
            derivedOverride.Name(); // will display "DerivedOverride"
            derivedNew.Name(); // will display "New"
            derivedOverWrite.Name(); // will display "OverWrite"
            Console.ReadLine();

            // with casting:
            baseClass.Name(); // will display "BaseClass" as before — this cannot be casted to anything since it's the base class 
            ((BaseClass)derivedOverride).Name(); // will display "DerivedOverride" because the original Name method has been overridden
            ((BaseClass)derivedNew).Name(); // will display "BaseClass"
            ((BaseClass)derivedOverWrite).Name(); // will display "BaseClass"
            Console.ReadLine();

            var t1 = typeof(BaseClass);
            Console.WriteLine(t1.Name);
            Console.WriteLine(t1.Assembly);
        }
    }


    internal class BaseClass
    {
        internal virtual void Name() // BaseClass defines a method called Name, which is marked as virtual to declare that our intention is that this method can be overriden by inheriting classes, but it doesn't have to be overriden, since there's an implementation there already which is not marked as abstract (ie, this implementation can be used)
        {
            Console.WriteLine("BaseClass");
        }
    }

    internal class DerivedOverride : BaseClass
    {
        internal override void Name() // DerivedOverride overrides (destroys and replaces) the Name method of BaseClass for the DerivedOverride class
        {
            Console.WriteLine("DerivedOverride");
        }
    }

    internal class DerivedNew : BaseClass
    {
        internal new void Name() // new creates a new method Name which has no relation to the original one. Now we cannot invoke the original Name method (in BaseClass) here in the DerivedNew class
        {
            Console.WriteLine("New");
        }
    }

    internal class DerivedOverwrite : BaseClass
    {
        internal void Name() // (no keyword) here we hide the base implementation of Name, but we haven't said that we intend to override and destroy (this is likely to cause a problem)
        {
            Console.WriteLine("Overwrite");
        }
    }
}