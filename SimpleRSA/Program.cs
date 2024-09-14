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
                int userInput;
                Console.Write(@"MENU:
[1] Generate Keys
[2] Encrypt message
[3] Decrypt message
[4] Escape
Enter: ");
                while (int.TryParse(Console.ReadLine(), out userInput) && (userInput != 1 && userInput != 2 && userInput != 3))
                    Console.Write(@"MENU:
[1] Generate Keys
[2] Encrypt message
[3] Decrypt message
[4] Escape
Enter: ");

                switch(userInput)
                {
                    case 1:
                        RSA.GenerateKeys();
                        Console.WriteLine($"Public key: {RSA.PublicKey}");
                        Console.WriteLine($"Private key: {RSA.PrivateKey}");
                        break;
                    case 2:
                        Console.Write("Message: ");
                        var message = Console.ReadLine();
                        while (message == string.Empty)
                        {
                            Console.Write("Message can't be empty, enter message: ");
                            message = Console.ReadLine();
                        }
                        RSA.Message = message;
                        Console.Write("Public key: ");
                        var publicKey = Console.ReadLine();
                        publicKey = publicKey.Trim('(', ')');
                        string[] partsOfPublicKey = publicKey.Split(',');
                        RSA.SetPublicKey(BigInteger.Parse(partsOfPublicKey[0]), BigInteger.Parse(partsOfPublicKey[1]));
                        RSA.EncryptMessage();
                        Console.WriteLine($"Encrypted message: {RSA.EncryptedMessage}");
                        break;
                    case 3:
                        Console.Write("Message to decrypt: ");
                        var messageToDecrypt = Console.ReadLine();
                        while (messageToDecrypt == string.Empty)
                        {
                            Console.Write("Message can't be empty, enter message: ");
                            message = Console.ReadLine();
                        }
                        RSA.SetMessageToDecrypt(messageToDecrypt);
                        Console.Write("Private key: ");
                        var privateKey = Console.ReadLine();
                        privateKey = privateKey.Trim('(', ')');
                        string[] partsOfPrivateKey = privateKey.Split(',');
                        RSA.SetPrivateKey(BigInteger.Parse(partsOfPrivateKey[0]), BigInteger.Parse(partsOfPrivateKey[1]));
                        RSA.DecryptMessage();
                        Console.WriteLine($"Decrypted message: {RSA.DecryptedMessage}");
                        break;
                    case 4:
                        return;
                }

                Console.WriteLine();
            }
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
