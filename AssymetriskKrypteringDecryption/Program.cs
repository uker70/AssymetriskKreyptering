using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssymetriskKrypteringDecryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\XmlKeys";
            //RSA.GenerateXmlKey(path);

            int runProgramChoice = 1;
            //loop to run the program
            while (runProgramChoice == 1)
            {
                byte[] text = null;
                byte[] decryptedText = null;

                //find txt files in the directory where the keys are
                string[] files = Directory.GetFiles(path, "*.txt");
                if (files.Length != 0)
                {
                    //writes the decrypter information
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                    {
                        rsa.PersistKeyInCsp = false;
                        rsa.FromXmlString(File.ReadAllText(path+@"\PrivateKey"));
                        Console.WriteLine($"Exponent: {BitConverter.ToString(rsa.ExportParameters(true).Exponent)}\n\n" +
                                          $"Modulus: {BitConverter.ToString(rsa.ExportParameters(true).Modulus)}\n\n" +
                                          $"D: {BitConverter.ToString(rsa.ExportParameters(true).D)}\n\n" +
                                          $"DP: {BitConverter.ToString(rsa.ExportParameters(true).DP)}\n\n" +
                                          $"DQ: {BitConverter.ToString(rsa.ExportParameters(true).DQ)}\n\n" +
                                          $"Inverse Q: {BitConverter.ToString(rsa.ExportParameters(true).InverseQ)}\n\n" +
                                          $"P: {BitConverter.ToString(rsa.ExportParameters(true).P)}\n\n" +
                                          $"Q: {BitConverter.ToString(rsa.ExportParameters(true).Q)}\n");
                    }

                    //gets a txt file and decrypt it
                    text = File.ReadAllBytes(files[0]);
                    decryptedText = RSA.Decrypt(path + @"\PrivateKey", text);

                    //writes the encrypted and decrypted text
                    Console.WriteLine("Encrypted Text:\n" + Convert.ToBase64String(text)+"\n");
                    Console.WriteLine("Decrypted Text:\n" + Encoding.UTF8.GetString(decryptedText)+"\n");

                    //deletes the decrypted txt file
                    File.Delete(files[0]);
                }
                else
                {
                    Console.WriteLine("There is no files to decrypt\n");
                }

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
