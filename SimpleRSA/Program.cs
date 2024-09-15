using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

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
            RSAKey rsaKey = new RSAKey();
            Console.WriteLine($"Public key: {rsaKey.PublicKey}");
            Console.WriteLine($"Private key: {rsaKey.PrivateKey}");
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

            Console.Write("Public key: ");
            var publicKey = Console.ReadLine();

            RSAKey rsaKey = new RSAKey();
            rsaKey.PublicKey = publicKey;

            Console.WriteLine($"Encrypted message: {RSA.EncryptMessage(rsaKey, message)}");
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

            Console.Write("Private key: ");
            var privateKey = Console.ReadLine();

            RSAKey rsaKey = new RSAKey();
            rsaKey.PrivateKey = privateKey;

            Console.WriteLine($"Decrypted message: {RSA.DecryptMessage(rsaKey, messageToDecrypt)}");
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
