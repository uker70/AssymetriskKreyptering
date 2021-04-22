using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssymetriskKrypteringDecryption
{
    class RSA
    {
        //decrypts the rsa encrypted text
        public static byte[] Decrypt(string privateKeyPath, byte[] text)
        {
            byte[] output;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                //tells rsa to not create a container
                rsa.PersistKeyInCsp = false;
                //reads the private key from xml
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                //decrypts
                output = rsa.Decrypt(text, false);
            }

            return output;
        }
    }
}
