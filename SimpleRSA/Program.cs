namespace SimpleRSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RSA.InitializePublicKey();
            Console.WriteLine("Public key - " + RSA.PublicKey);
            Console.WriteLine("Private key - " + RSA.PrivateKey);
        }
    }
}
