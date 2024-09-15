using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRSA
{
    public class RSAKey
    {
        public static Random Random = new Random();

        private List<int> _primes => new PrimeNumbers().ToList();
        //private static List<BigInteger> _primes => new List<BigInteger>() { 761, 911 };

        public BigInteger _p, _q, _n, _phi, _e, _d;

        private string _publicKey;
        public string PublicKey
        {
            get
            { 
                return _publicKey;
            }
            set
            {
                if (!TryParseKey(value, out BigInteger exponent, out BigInteger modulus))
                {
                    Console.WriteLine("Invalid public key format.");
                    return;
                }
                _e = exponent;
                _n = modulus;
                _publicKey = value;
            }
        }

        private string _privateKey;
        public string PrivateKey
        {
            get
            {
                return _privateKey;
            }
            set
            {
                if (!TryParseKey(value, out BigInteger exponent, out BigInteger modulus))
                {
                    Console.WriteLine("Invalid private key format.");
                    return;
                }
                _d = exponent;
                _n = modulus;
                _privateKey = value;
            }
        }

        public RSAKey()
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

                PublicKey = "(" + _e.ToString() + "," + _n.ToString() + ")";
                PrivateKey = "(" + _d.ToString() + "," + _n.ToString() + ")";
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
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

        private static BigInteger _greatestCommonDivisor(BigInteger a, BigInteger b)
        {
            BigInteger t;

            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            };
            return a;
        }

        private static BigInteger _reverseMod(BigInteger a, BigInteger n)
        {
            BigInteger a0, n0, p0, p1, q, r, t;

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

        public void GetDiagnosticData()
        {
            Console.Write($"\nPrimes used to generate keys: {_p}, {_q}.\n" +
                $"e: {_e} d: {_d}\n" +
                $"n: {_n}\n" +
                $"phi: {_phi}\n");
        }
    }
}
