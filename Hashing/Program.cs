using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // hashing - one-way encryption
            
            // this represents a hashed password stored in a database
            var storedPasswordHash = new byte[] // pre-generated hash array based on "P4ssw0rd!" string from below
                {
                    148, 152, 235, 251, 242, 51, 18, 100, 176, 51, 147, 
                    249, 128, 175, 164, 106, 204, 48, 47, 154, 75, 
                    82, 83, 170, 111, 8, 107, 51, 13, 83, 2, 252
                };

            var password = Encoding.Unicode.GetBytes("P4ssw0rd!");
            var passwordHash = SHA256.Create().ComputeHash(password); // this generates a byte array for the hash of the "P4ssw0rd!" string

            // nice convenience method - can also supply a custom comparator
            if (passwordHash.SequenceEqual(storedPasswordHash)) // compare the pre-generated storedPasswordHash array to the passwordHash array. SequenceEqual is an extension method we're using here. passwordHash implements enumerable, so SequenceEquals applies to that
            {
                Console.WriteLine("Passwords match!");
            }

            var sb = new StringBuilder("passwordHash byte[] { ");
            foreach (var b in passwordHash)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            Console.WriteLine(sb.ToString());

            Console.Read();
        }
    }
}
