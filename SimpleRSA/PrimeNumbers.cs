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
        private const int _primesLimit = 1000000;
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 10; i <= _primesLimit; i++)
            {
                if (IsPrime(i))
                {
                    yield return i;
                }
            }          
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
