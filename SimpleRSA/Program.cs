namespace SimpleRSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIIArtLogo();

            RSA.GenerateKeys();
            RSA.GetPrimes();
            Console.WriteLine("Public key - " + RSA.PublicKey);
            Console.WriteLine("Private key - " + RSA.PrivateKey);

            Console.WriteLine();

            RSA.Message = "Lorem ipsum. Consequatur deserunt minima commodi eveniet eum totam sit. Odit provident placeat accusantium odit consequatur et. Voluptas neque non quaerat consequatur laudantium voluptates.";
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
