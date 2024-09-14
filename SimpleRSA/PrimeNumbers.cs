using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRSA
{
    public class PrimeNumbers : IEnumerable<int>
    {
        private readonly int _lowerLimit;
        private readonly int _upperLimit;

        public PrimeNumbers(int lowerLimit = 1000, int upperLimit = 1000000)
        {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var prime in SieveOfEratosthenes(_upperLimit))
            {
                if (prime >= _lowerLimit)
                {
                    yield return prime;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static List<int> SieveOfEratosthenes(int limit)
        {
            bool[] isPrime = new bool[limit + 1];
            for (int i = 2; i <= limit; i++)
            {
                isPrime[i] = true;
            }

            for (int i = 2; i * i <= limit; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            List<int> primes = new List<int>();
            for (int i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
