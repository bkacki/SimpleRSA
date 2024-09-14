using System;
using System.Numerics;

namespace SimpleRSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIIArtLogo();

            while (true)
            {
                DisplayMenu();

                int userInput;
                if (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 1 || userInput > 4)
                {
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                    continue;
                }

                switch (userInput)
                {
                    case 1:
                        GenerateKeys();
                        break;
                    case 2:
                        EncryptMessage();
                        break;
                    case 3:
                        DecryptMessage();
                        break;
                    case 4:
                        return;
                }

                Console.WriteLine();
            }
        }

        static void DisplayMenu()
        {
            Console.Write(@"MENU:
[1] Generate Keys
[2] Encrypt message
[3] Decrypt message
[4] Escape
Enter: ");
        }

        static void GenerateKeys()
        {
            RSA.GenerateKeys();
            Console.WriteLine($"Public key: {RSA.PublicKey}");
            Console.WriteLine($"Private key: {RSA.PrivateKey}");
        }

        static void EncryptMessage()
        {
            Console.Write("Message: ");
            var message = Console.ReadLine();
            while (string.IsNullOrEmpty(message))
            {
                Console.Write("Message can't be empty, enter message: ");
                message = Console.ReadLine();
            }

            RSA.Message = message;
            Console.Write("Public key (format: (number1,number2)): ");
            var publicKey = Console.ReadLine();

            if (!TryParseKey(publicKey, out BigInteger exponent, out BigInteger modulus))
            {
                Console.WriteLine("Invalid public key format.");
                return;
            }

            RSA.SetPublicKey(exponent, modulus);
            RSA.EncryptMessage();
            Console.WriteLine($"Encrypted message: {RSA.EncryptedMessage}");
        }

        static void DecryptMessage()
        {
            Console.Write("Message to decrypt: ");
            var messageToDecrypt = Console.ReadLine();
            while (string.IsNullOrEmpty(messageToDecrypt))
            {
                Console.Write("Message can't be empty, enter message: ");
                messageToDecrypt = Console.ReadLine();
            }

            RSA.SetMessageToDecrypt(messageToDecrypt);
            Console.Write("Private key (format: (number1,number2)): ");
            var privateKey = Console.ReadLine();

            if (!TryParseKey(privateKey, out BigInteger exponent, out BigInteger modulus))
            {
                Console.WriteLine("Invalid private key format.");
                return;
            }

            RSA.SetPrivateKey(exponent, modulus);
            RSA.DecryptMessage();
            Console.WriteLine($"Decrypted message: {RSA.DecryptedMessage}");
        }

        static bool TryParseKey(string key, out BigInteger exponent, out BigInteger modulus)
        {
            exponent = 0;
            modulus = 0;
            if (string.IsNullOrWhiteSpace(key)) return false;

            key = key.Trim('(', ')');
            string[] parts = key.Split(',');
            if (parts.Length != 2) return false;

            return BigInteger.TryParse(parts[0], out exponent) && BigInteger.TryParse(parts[1], out modulus);
        }

        static void ASCIIArtLogo()
        {
            Console.WriteLine(@"
    ooooooooo.   .oooooo..o       .o.       
   `888   `Y88. d8P'    `Y8      .888.      
    888   .d88' Y88bo.          .8``888.     
    888ooo88P'   ``Y8888o.     .8' `888.    
    888`88b.        ```Y88b   .88ooo8888.   
    888  `88b.  oo     .d8P  .8'     `888.  
    o888o  o888o 88888888P'  o88o     o8888o 
");
        }
    }
}
