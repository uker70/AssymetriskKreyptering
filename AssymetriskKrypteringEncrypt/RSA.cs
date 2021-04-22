using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssymetriskKrypteringEncrypt
{
    class RSA
    {
        //checks if xml keys already exist, and if they dont, then creates them
        public static void GenerateXmlKey(string keyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                if (!Directory.Exists(keyPath))
                {
                    Directory.CreateDirectory(keyPath);
                }

                if (!File.Exists(keyPath + @"\PublicKey"))
                {
                    File.WriteAllText(keyPath+@"\PublicKey", rsa.ToXmlString(false));
                }

                if (!File.Exists(keyPath + @"\PrivateKey"))
                {
                    File.WriteAllText(keyPath+@"\PrivateKey", rsa.ToXmlString(true));
                }
            }
        }
        //encrypts the text with rsa encryption
        public static byte[] Encrypt(string publicKeyPath, byte[] text)
        {
            byte[] output;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                //tells rsa to not create a container
                rsa.PersistKeyInCsp = false;
                //reads the public key from xml
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                //encrypts the text
                output = rsa.Encrypt(text, false);
            }

            return output;
        }
    }
}
