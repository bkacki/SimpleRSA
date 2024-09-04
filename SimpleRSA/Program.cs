namespace SimpleRSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RSA.InitializePublicKey();
            Console.WriteLine("Public key - " + RSA.PublicKey);
            Console.WriteLine("Private key - " + RSA.PrivateKey);

            Console.WriteLine();

            RSA.Message = "Rucham psa jak sra.";
            Console.WriteLine($"Message to encrypt - {RSA.Message}");
            RSA.EncryptMessage();
            Console.WriteLine($"Encrypted message - {RSA.EncryptedMessage}");
            RSA.DecryptMessage();
            Console.WriteLine($"Decrypted message - {RSA.DecryptedMessage}");
        }
    }
}
