using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Symmetric
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dataToProtect = "This is a bunch of super secret content!";
            var dataToProtectAsArray = Encoding.Unicode.GetBytes(dataToProtect);

            // Symmetric encryption

            // Uses Rijndael as an algorithm
            // two classes Rijndael and Aes - use Aes (more secure)

            // array of 16 random bytes - must be used for decryption
            // should be secret
            var key = new byte[] { 12, 2, 56, 117, 12, 67, 33, 23, 12, 2, 56, 117, 12, 67, 33, 23 }; // the larger the key, the more heavily encrypted your code will be and the less likely brute force attacks will ever be able to decrypt it. 16 bytes is usually sufficiently secure

            // another list of 16 bytes 
            // can be shared publically
            // should be changed for each message exchange (this is like an additional password)
            var initializationVector = new byte[] { 37, 99, 102, 23, 12, 22, 156, 204, 11, 12, 23, 44, 55, 1, 157, 233 }; // you can use a common key that you've shared, but you can also embed an initialization vector into each message that you send backwards and forwards to add another degree of randomness to the encryption. Just keep changing that initialization vector, and it makes it harder to break the encryption

            byte[] symEncryptedData; // declare byte array to hold the encrypted data

            // save for reuse
            var algorithm = Aes.Create(); // create and instance of our algorithm. We're not using using() yet here, because we want to save this for reuse down below

            // encrypt
            // 3 usings back-to-back. This is handy syntax for nesting using statements.
            // Here's we have 3 levels of disposable objects, and we're implying a lifecycle that cryptoStream is dependent on memoryStream, which is dependent on encryptor. So as we roll out of this, these things will be disposed of in that order as well
            using (var encryptor = algorithm.CreateEncryptor(key, initializationVector)) // it odes work to use the same key here for the key and the initializationVector, but it defeats the purpose of using the initialization vector
            using (var memoryStream = new MemoryStream()) // we want to encrypt things in memory, so create a memory stream, where the encrypted data gets written to. This is similar to Windows Data Protection. The difference between a byte array an a memory stream is that a memory stream will automatically size itself based on the content that's being written to whereas a byte array has finite bounds (you define it upfront)
            using (var cryptoStream = new CryptoStream( // cryptography stream, which accepts:
                memoryStream, // the memory stream
                encryptor, // the encryptor that we will use to encrypt the content
                CryptoStreamMode.Write)) // the mode that it will write to this stream (this is a wrapper around the stream)
            {
                cryptoStream.Write(dataToProtectAsArray, // we write the dataToProtect through the cryptoStream to the memoryStream by donig the encryption
                    0, // offset for where we want to start in the array (here, we're starting from the beginning)
                    dataToProtectAsArray.Length); // how far we run through in the array (here, we're running through the entir length of the array)
                cryptoStream.FlushFinalBlock(); // streams generally write in chunks, so flush the final block even if it's an incomplete block
                symEncryptedData = memoryStream.ToArray(); // get the memory stream, convert it to an array (it takes that buffer little variable size and outputs it as an array), and write it to symEncryptedData, which is the byte array declared previously
            } // the symEncryptedData byte array can then be written somewhere, e.g. put in a database, a file, etc. 

            // decrypt
            byte[] symUnencryptedData; // declare byte array to hold the unecrypted data
            using (var decryptor = algorithm.CreateDecryptor(key, initializationVector)) // now we create a decryptor instead of an encryptor, but we pass in the same key and intialization vector as we used above
            using (var memoryStream = new MemoryStream()) // new memoryStream again
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write)) // now we pass in the decryptor to the cryptoStream, and we're writing to our memoryStream again
            {
                cryptoStream.Write(symEncryptedData, 0, symEncryptedData.Length); // takes our encrypted data from before and is length
                cryptoStream.FlushFinalBlock(); // flush out the final block (even if it's incomplete)
                symUnencryptedData = memoryStream.ToArray(); // write the uncrypted data back to symUnencryptedData
            }

            algorithm.Dispose();

            if (dataToProtectAsArray.SequenceEqual(symUnencryptedData)) // .SequenceEqual to compare the 2 arrays: original byte array dataToProtectAsArray and the byte array holding the unencrypted data symUnencryptedData and makre sure they have exactly the same contents
            {
                Console.WriteLine("Symmetric encrypted values match!"); // we have succesfully encrypted and decrypted the data (both sides of the conversation)
            }

        }
    }
}
