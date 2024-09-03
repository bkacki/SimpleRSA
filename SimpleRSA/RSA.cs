using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRSA
{
    public static class RSA
    {
        public static Random Random = new Random();

        private static List<int> _primes => new PrimeNumbers().ToList();
        private static int _p, _q, _n, _phi, _e, _d;

        private static string _publicKey;
        public static string PublicKey 
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

                    for (_e = 3; _greatestCommonDivisor(_e, _phi) != 1; _e += 2)
                        _d = _greatestCommonDivisor(_e, _phi);

                    _publicKey = "(" + _e.ToString() + "," + _n.ToString() + ")";
                    PrivateKey = "(" + _d.ToString() + "," + _n.ToString() + ")";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public static string PrivateKey { get; private set; }

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
    }
}
