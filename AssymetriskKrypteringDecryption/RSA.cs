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
        public static byte[] Decrypt(string privateKeyPath, byte[] text)
        {
            byte[] output;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                output = rsa.Decrypt(text, false);
            }

            return output;
        }
    }
}
