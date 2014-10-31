namespace HandlingExceptions
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Process();
            }
            catch (DivideByZeroException ex)
            {
                // specific exception
                // for a divide-by-zero exception you might want to handle it by setting the value to 0, for example
            }
            catch (Exception ex) // this will catch any exception
            {
                // generic exception
            }
            finally
            {
                // this will always occur
            }
        }

        private static void Process()
        {
            throw new NotImplementedException();
        }
    }
}
