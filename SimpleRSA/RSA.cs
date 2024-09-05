using System.Text;

namespace SimpleRSA
{
    public static class RSA
    {
        public static Random Random = new Random();

        //private static List<int> _primes => new PrimeNumbers().ToList();
        private static List<int> _primes => new List<int>() { 13, 11 };

        private static int _p, _q, _n, _phi, _e, _d;

        private static string? _publicKey;
        public static string? PublicKey
        {
            get
            {
                return _publicKey;
            }
            private set
            {
                try
                {
                    do
                    {
                        _p = _primes[Random.Next(_primes.Count)];
                        _q = _primes[Random.Next(_primes.Count)];
                    } while (_p == _q);
                    _phi = (_p - 1) * (_q - 1);
                    _n = _p * _q;

                    for (_e = 3; _greatestCommonDivisor(_e, _phi) != 1; _e += 2) ;

                    _d = _reverseMod(_e, _phi);

                    _publicKey = "(" + _e.ToString() + "," + _n.ToString() + ")";
                    PrivateKey = "(" + _d.ToString() + "," + _n.ToString() + ")";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public static string? PrivateKey { get; private set; }

        private static int _greatestCommonDivisor(int a, int b)
        {
            int t;

            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            };
            return a;
        }

        private static int _reverseMod(int a, int n)
        {
            int a0, n0, p0, p1, q, r, t;

            p0 = 0; p1 = 1; a0 = a; n0 = n;
            q = n0 / a0;
            r = n0 % a0;
            while (r > 0)
            {
                t = p0 - q * p1;
                if (t >= 0)
                    t = t % n;
                else
                    t = n - ((-t) % n);
                p0 = p1; p1 = t;
                n0 = a0; a0 = r;
                q = n0 / a0;
                r = n0 % a0;
            }
            return p1;
        }

        public static void InitializePublicKey()
        {
            PublicKey = "Initialize";
        }

        public static string? Message { get; set; }

        private static string? _encryptedMessage;
        public static string? EncryptedMessage
        {
            get
            {
                return _encryptedMessage;
            }
            private set
            {
                byte[] encoded = Encoding.UTF8.GetBytes(Message!);
                byte[] encrypted = new byte[encoded.Length];
                for (int i = 0; i < encoded.Length; i++)
                {
                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    byte[] bytes = encoding.GetBytes(encoded[i].ToString());
                }
                _encryptedMessage = Encoding.UTF8.GetString(encrypted);
            }
        }
        public static void EncryptMessage()
        {
            EncryptedMessage = "Encrypt";
        }

        private static string? _decryptedMessage;
        public static string? DecryptedMessage
        {
            get
            {
                return _decryptedMessage;
            }
            private set
            {
                byte[] encoded = Encoding.UTF8.GetBytes(_encryptedMessage!);
                byte[] decrypted = new byte[encoded.Length];
                for (int i = 0; i < encoded.Length; i++)
                    decrypted[i] = (byte)RSAEncrypt(encoded[i], _d, _n);

                _decryptedMessage = Encoding.UTF8.GetString(decrypted);
            }
        }
        public static void DecryptMessage()
        {
            DecryptedMessage = "Decrypt";
        }

        private static int _modularExponentiation(int pow, int q, int n)
        {
            int result = 1;

            for (int i = q; i > 0; i /= 2)
            {
                if ((i % 2) == 1)
                    result = (result * pow) % n;
                pow = (pow * pow) % n;
            }
            return result;
        }

        public static int RSAEncrypt(int message, int e, int n)
        {
            return _modularExponentiation(message, e, n);
        }
    }
}
