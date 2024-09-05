using System.Collections;

namespace SimpleRSA
{
    public class PrimeNumbers : IEnumerable<int>
    {
        private const int _primesLimit = 100;
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 2; i <= _primesLimit; i++)
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
