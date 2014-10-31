using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WindowsDataProtection
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dataToProtect = "This is a bunch of super secret content!";
            var dataToProtectAsArray = Encoding.Unicode.GetBytes(dataToProtect); // takes our string, applies Unicode encoding (16 bits that can be expressed in the Unicode char set), gets those bits and converts them to a Byte array. We're using byte arrays because we want to bring everything back to numbers/numerics that we can interact with

            // Windows Data Protection (we can also protect for the LocalMachine too)
            /* needed to add a framework reference to the project for System.Security to access System.Security.Cryptography.ProtectedData
            ProtectedData is the standard class (within the Cryptography namespace), providing methods for protecting and unprotecting data*/
            var wdpEncryptedData = ProtectedData.Protect(
                dataToProtectAsArray,
                null, // optional byte array we can use that will vary the level of encryption (additional entropy)—by putting in additional random sets of bytes, you can offset how the encryption is placed to include and extra level of protection should you so wish. It's common to replace byte[] with null
                DataProtectionScope.CurrentUser // scope: if we say DataProtectionScope.CurrentUser, then you have to be logged-on as my user account and anything that I encrypt in this method, so that I can then decrypt it. You can also use DataProtectionScope.LocalMachine, which would allow anyone who logs on to this machine to decrypt this data
                ); 

            var wdpUnEncryptedData = ProtectedData.Unprotect(
                wdpEncryptedData, 
                null, 
                DataProtectionScope.CurrentUser
                );
            var wdpUnencryptedString = Encoding.Unicode.GetString(wdpUnEncryptedData);

            Debug.Assert(dataToProtect.Equals(wdpUnencryptedString));
        }
    }
}
