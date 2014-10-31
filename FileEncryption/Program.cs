using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FileEncryption
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string dataToProtect = "This is a bunch of super secret content!";

            // file encryption

            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"MyDataFile.txt"); // (unrelated to encryption) creating a file name to access. We're putting the file in a special folder

            // Encrypt a file in the file system
            File.WriteAllText(fileName, dataToProtect); // (unrelated to encryption) this is a static method we're using on the File class to write out to a file name the data to protect. The data will then be contained in this file on the file system

            // now we can encrypt it - only we can access it now
            File.Encrypt(fileName);

        }
    }
}