namespace SimpleRSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIIArtLogo();

            RSA.GenerateKeys();
            Console.WriteLine("Public key - " + RSA.PublicKey);
            Console.WriteLine("Private key - " + RSA.PrivateKey);
            RSA.GetDiagnosticData();

            Console.WriteLine();

            RSA.Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Console.WriteLine($"Message to encrypt - {RSA.Message}");
            RSA.EncryptMessage();
            Console.WriteLine($"\nEncrypted message - {RSA.EncryptedMessage}");
            RSA.DecryptMessage();
            Console.WriteLine($"\nDecrypted message - {RSA.DecryptedMessage}");
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
