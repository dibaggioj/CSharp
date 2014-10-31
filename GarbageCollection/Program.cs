using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            // while the following is executing, everything else in the applications stops and waits
            GC.Collect(); // GC represents the global garbage collector
            GC.WaitForPendingFinalizers();
            GC.Collect();
             
        }
    }
}

interface IDisposable
{
    void Dispose();
}

public class Demo : IDisposable // Demo class implements IDisposable
{
    public void Dispose() // Demo class has an implementation of Dispose
    {
        // release resources
    }
}

public class AdvancedDemo : IDisposable // implement IDisposable
{
    public void Dispose() // method returns void
    {
        Dispose(true); // call another implementation of Dispose, with a true parameter which matches a disposing parameter
        GC.SuppressFinalize(this); // prevent finalizer from trying to rerun and redo the dispose
    }

    protected virtual void Dispose(bool disposing) // method returns void
    {
        if (disposing) // if disposing == true
        {
            // release managed resources. Here, other managed types that have the dispose pattern. We'll call Dispose on those. So we have a cascade of Dispose down our Object Graph (our related objects)
        }
        // release unmanaged resources. Set to null our external references, for example, COM objects, etc. So we release our reference on those as well
    }
    ~AdvancedDemo() // this is a finalizer, which is the inverse of a constructor; allows us to perform operations at the end of the lifecycle of a managed type. Not very common that we need to do this; it's usually just when releasing resources
    {
        Dispose(false);
    }
}
//