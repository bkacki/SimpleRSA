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
        public static Random Random = new Random();

        private static List<int> _primes => new PrimeNumbers().ToList();
        //private static List<BigInteger> _primes => new List<BigInteger>() { 761, 911 };

        private static BigInteger _p, _q, _n, _phi, _e, _d;
        public static string PublicKey { get; private set; }
        public static string PrivateKey { get; private set; }

        public static string Message { get; set; }

        private static string _encryptedMessage;
        public static string EncryptedMessage
        {
            get
            {
                return _encryptedMessage;
            }
            private set
            {
                byte[] encoded = Encoding.UTF8.GetBytes(Message);
                List<BigInteger> encryptedBlocks = new List<BigInteger>();

                // Calculate the block size based on the modulus
                int blockSize = (int)Math.Floor(BigInteger.Log(_n, 256)) - 1; // Reduce by 1 for padding

                // Encrypt in blocks
                for (int i = 0; i < encoded.Length; i += blockSize)
                {
                    byte[] block = encoded.Skip(i).Take(blockSize).ToArray();
                    BigInteger blockValue = new BigInteger(new byte[] { 0 }.Concat(block).ToArray());

                    if (blockValue >= _n)
                    {
                        throw new Exception("Block value is larger than modulus n. Encryption aborted.");
                    }

                    BigInteger encryptedBlock = RSAEncrypt(blockValue, _e, _n);
                    encryptedBlocks.Add(encryptedBlock);
                }

                // Convert the encrypted blocks to a Base64 string
                List<byte> encryptedBytes = new List<byte>();
                foreach (var encryptedBlock in encryptedBlocks)
                {
                    byte[] blockBytes = encryptedBlock.ToByteArray();

                    // Ensure consistent byte length for each block
                    int byteLength = (int)Math.Ceiling((double)_n.GetByteCount());
                    if (blockBytes.Length < byteLength)
                    {
                        blockBytes = new byte[byteLength - blockBytes.Length].Concat(blockBytes).ToArray();
                    }

                    encryptedBytes.AddRange(blockBytes);
                }

                _encryptedMessage = Convert.ToBase64String(encryptedBytes.ToArray());

                /*byte[] encoded = Encoding.UTF8.GetBytes(Message);
                List<byte> encryptedBytes = new List<byte>();

                // Szyfruj każdy bajt osobno
                foreach (byte b in encoded)
                {
                    int encryptedBlock = (int)RSAEncrypt(b, _e, _n); // Szyfrujemy każdy bajt
                    encryptedBytes.AddRange(BitConverter.GetBytes(encryptedBlock)); // Dodajemy zaszyfrowane bajty
                }

                _encryptedMessage = Convert.ToBase64String(encryptedBytes.ToArray());*/
            }
        }
        public static void EncryptMessage()
        {
            EncryptedMessage = "Encrypt";
        }

        private static string _decryptedMessage;
        public static string DecryptedMessage
        {
            get
            {
                return _decryptedMessage;
            }

            private set
            {
                byte[] encryptedBytes = Convert.FromBase64String(_encryptedMessage);
                List<byte> decryptedBytes = new List<byte>();

                // Calculate byte length for each block
                int byteLength = (int)Math.Ceiling((double)_n.GetByteCount());

                // Decrypt each block
                for (int i = 0; i < encryptedBytes.Length; i += byteLength)
                {
                    byte[] blockToCheck = encryptedBytes.Skip(i).Take(byteLength).ToArray();
                    byte[] block = blockToCheck.SkipWhile(x => x == 0).ToArray();
                    BigInteger encryptedBlock = new BigInteger(block);
                    BigInteger decryptedBlock = RSAEncrypt(encryptedBlock, _d, _n);

                    byte[] decryptedBlockBytes = decryptedBlock.ToByteArray();

                    // Ensure consistent block size handling
                    int blockSize = (int)Math.Floor(BigInteger.Log(_n, 256)) - 1; // Original block size
                    if (decryptedBlockBytes.Length < blockSize)
                    {
                        decryptedBlockBytes = new byte[blockSize + 1 - decryptedBlockBytes.Length].Concat(decryptedBlockBytes).ToArray();
                    }

                    decryptedBytes.AddRange(decryptedBlockBytes.SkipWhile(b => b == 0)); // Skip leading zeros

                    bool equalBlocks = (String.Join(" ", blockToCheck) == String.Join(" ", block)) ? true : false;
                    Console.WriteLine($"Diagnosis data: \n" +
                        $"Are equal: {equalBlocks}\n" +
                        $"BlockToCheck: {String.Join(" ", blockToCheck)}\n" +
                        $"Block: {String.Join(" ", block)}\n" +
                        $"Byte length: {byteLength}\n" +
                        $"block size: {blockSize}\n" +
                        $"encrypted block: {encryptedBlock}\n" +
                        $"decrypted block: {decryptedBlock}\n" +
                        $"decrypted block bytes: {Encoding.UTF8.GetString(decryptedBlockBytes)}\n");
                }

                _decryptedMessage = Encoding.UTF8.GetString(decryptedBytes.ToArray()).TrimEnd('\0');

                /*byte[] encryptedBytes = Convert.FromBase64String(_encryptedMessage);
                List<byte> decryptedBytes = new List<byte>();

                // Deszyfruj każdy blok 4-bajtowy (bo zaszyfrowane wartości to inty)
                for (int i = 0; i < encryptedBytes.Length; i += 4)
                {
                    byte[] block = encryptedBytes.Skip(i).Take(4).ToArray();
                    int encryptedBlock = BitConverter.ToInt32(block, 0);
                    int decryptedBlock = (int)RSAEncrypt(encryptedBlock, _d, _n);

                    // Dodaj odszyfrowany bajt do listy
                    decryptedBytes.Add((byte)decryptedBlock);
                }

                _decryptedMessage = Encoding.UTF8.GetString(decryptedBytes.ToArray());*/
            }

        }
        public static void DecryptMessage()
        {
            DecryptedMessage = "Decrypt";
        }

        public static void GenerateKeys()
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

        public static void GetDiagnosticData()
        {
            Console.Write($"\nPrimes used to generate keys: {_p}, {_q}.\n" +
                $"e: {_e} d: {_d}\n" +
                $"n: {_n}\n" +
                $"phi: {_phi}\n");
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