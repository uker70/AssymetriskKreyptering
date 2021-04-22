using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssymetriskKrypteringEncrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\XmlKeys";
            RSA.GenerateXmlKey(path);

            int runProgramChoice = 1;
            //loop to run the program
            while (runProgramChoice == 1)
            {
                string text = "";
                byte[] encryptedText = null;

                //writes the exponent and modulus values of the encrypter
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(File.ReadAllText(path + @"\PrivateKey"));
                    Console.WriteLine($"Exponent: {BitConverter.ToString(rsa.ExportParameters(false).Exponent)}\n\n" +
                                      $"Modulus: {BitConverter.ToString(rsa.ExportParameters(false).Modulus)}\n");
                }

                //asks for text to encrypt and encrypts it
                Console.WriteLine("Write the text to encrypt");
                text = Console.ReadLine();
                encryptedText = RSA.Encrypt(path + @"\PublicKey", Encoding.UTF8.GetBytes(text));

                //writes the encrypted text to a txt file in the same folder as the keys
                File.WriteAllBytes(path+@"\"+DateTime.Now.Ticks+".txt", encryptedText);

                Console.WriteLine("1. Try again\n2. Exit");
                runProgramChoice = MenuChoose(1, 2);
                Console.Clear();
            }
        }

        //menu to select login or create user
        private static int MenuChoose(int numbOne, int numbTwo)
        {
            int input = 0;
            while (input < numbOne || input > numbTwo)
            {
                try
                {
                    Console.Write("\nChoose: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                { }
            }

            return input;
        }
    }
}
