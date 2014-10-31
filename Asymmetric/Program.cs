using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Asymmetric
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dataToProtect = "This is a bunch of super secret content!";
            var dataToProtectAsArray = Encoding.Unicode.GetBytes(dataToProtect);

            // Asymmetric (or Public Key) Encryption

            byte[] signature;
            byte[] publicAndPrivateKey;
            byte[] publicKeyOnly;
            var hashImplementation = SHA1.Create();

            // create a signature, create our public and private keys - we could save these out as XML, etc.
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                signature = rsaProvider.SignData(dataToProtectAsArray, hashImplementation);
                publicAndPrivateKey = rsaProvider.ExportCspBlob(true);
                publicKeyOnly = rsaProvider.ExportCspBlob(false);
            }

            // create a new RSA
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                // import our public key
                rsaProvider.ImportCspBlob(publicKeyOnly);

                // has it been tampered with?
                if (!rsaProvider.VerifyData(dataToProtectAsArray, hashImplementation, signature))
                {
                    Console.WriteLine("Data has been tampered with");
                }

                // now let's tamper with our data

                dataToProtectAsArray[5] = 255;
                if (!rsaProvider.VerifyData(dataToProtectAsArray, hashImplementation, signature))
                {
                    Console.WriteLine("Data has been tampered with");
                }
            }

            hashImplementation.Dispose();
        
        }
    }
}
