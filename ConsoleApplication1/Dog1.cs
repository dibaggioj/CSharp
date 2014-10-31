using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /* Event subscriber */
    public class Trainer
    {
        void Operate()
        {
            var dog = new Poodle(); // create an instance of Poodle
            dog.HasSpoken += dog_HasSpoken;  // subscribe to the event — the "+=" means "add-to" so it's shorthand here since an event is a list of methods that need to be raised back
        }

        void dog_HasSpoken(object sender, EventArgs e)
        {
 	        throw new NotImplementedException();
        }
    }

    public abstract partial class Dog
    {
        /* Properties */
        //public string Name { get; set; } //properties hold values. get and set instruct the compiler to create an infrastructure for us to store the property values

        private string _name;
        public string Name //full implementation
        {
            get { return _name; }
            private set { // although the scope of the dog’s name is shared publicly for consumption, it can only be created from within the class itself 
                _name = value; // look at value 
            }
        }

        /* Events */
        public event EventHandler HasSpoken; //full implementation for an event


        /* Methods */
        public void Speak(string what = "bark") //"bark" is the default what variable you ask a dog to speak
        {
            // TODO
            //Console.WriteLine(what);
            // event:
            if (HasSpoken != null) // check to make sure somebody is subscribed to the event (i.e., somebody's listening)
                HasSpoken(this, EventArgs.Empty);
        }
        //note: the unique signature above is a publicly accessible method that doesn't return anything (void), is named Speak, and takes a string argument
        //you can't have another method with the exact same signature, so you could have another method for example that takes an additional parameter, as follows:
        public void Speak(int times, string what = "bark", bool sit = true) // this method won't conflict with the previous method above, because it has additional/different parameters. It has a few signature, because of the defaults passed-in; for example, you can just call this.Speak(2) when implementing it, since this argument isn't a string, and it actually then is calling this.Speak(2,"bark",true)
        {
            // TODO
            //Console.WriteLine("{0}, {1}, and {2}", times, what, sit);

        }

        private void Foo() { } //can be used only by this class (this is the beginning of encapsulation)

        protected void Bar() { } //can be used only by this class and derived classes (ones that inherited from this class)
        
        internal void Daw() { } //can be used inside the same assembly (it’s scoped to a project in Visual Studio)
        }

        /* Inheritance */
        class Poodle: Dog //Poodle class inherits from Dog
        {
        public void x() {
            this.Bar();
            this.Daw(); 
            //Foo won’t show up as available, because it's private and can only be used by the Dog class
            //note: the above two methods execute inside the object, and the object is both Dog and Poodle
            this.Speak(); // this will call Speak("bark"), the first Speak method
            this.Speak("woof"); // this call Speak("woof"), the first Speak method
            this.Speak(2); // this will call Speak(2, 'bark', true), the second Speak method
            //this.Speak(2,true); // this will fail, because the compiler is expecting a string for the second parameter of the second Speak method
            this.Speak(3, sit:false); // this will bypass the string argument 'what' an call Speak(3, 'bark', false), the second Speak method
            this.Speak(4, "woof"); // this will call Speak(4, "woof", true), the second Speak method
            this.Speak(5, what:"woof"); // this will call Speak(4, "woof", true), the second Speak method, but note: we didn't actually need to bypass anything, so the order there parameters were declared in doesn't actually matter
            this.Speak(6, "woof", false); // this will call Speak(5, "woof", false), the second Speak method
            /*inefficient:*/ this.Speak(7, "bark", true); // this will call Speak(6, "bark", true), but it is inefficient, since we have defaults specified
        }
            
    }
}
