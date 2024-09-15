using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SimpleRSA
{
    public static class RSA
    {
        public static string EncryptMessage(RSAKey rsaKey, string message)
        {
            if(rsaKey._e == null || rsaKey._n == null || rsaKey.PublicKey == "Invalid public key format.")
                return "Invalid public key.";

            return BlockEncryption.EncryptWithBlocks(rsaKey, message);
        }

        public static string DecryptMessage(RSAKey rsaKey, string message)
        {
            if (rsaKey._d == null || rsaKey._n == null || rsaKey.PrivateKey == "Invalid private key format.")
                return "Invalid private key.";

            return BlockEncryption.DecryptWithBlocks(rsaKey, message);
        }

        private static BigInteger _modularExponentiation(BigInteger pow, BigInteger q, BigInteger n)
        {
            BigInteger result = 1;

            for (BigInteger i = q; i > 0; i /= 2)
            {
                if ((i % 2) == 1)
                    result = (result * pow) % n;
                pow = (pow * pow) % n;
            }
            return result;
        }

        public static BigInteger RSAEncrypt(BigInteger message, BigInteger e, BigInteger n)
        {
            return _modularExponentiation(message, e, n);
        }
    }
}