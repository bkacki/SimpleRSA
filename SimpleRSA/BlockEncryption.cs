using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRSA
{
    public static class BlockEncryption
    {
        public static string EncryptWithBlocks(RSAKey rsaKey, string message)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(message);
            List<BigInteger> encryptedBlocks = new List<BigInteger>();

            // Calculate the block size based on the modulus
            int blockSize = (int)Math.Floor(BigInteger.Log(rsaKey._n, 256)) - 1; // Reduce by 1 for padding

            // Encrypt in blocks
            for (int i = 0; i < encoded.Length; i += blockSize)
            {
                byte[] block = encoded.Skip(i).Take(blockSize).ToArray();
                BigInteger blockValue = new BigInteger(new byte[] { 0 }.Concat(block).ToArray());

                if (blockValue >= rsaKey._n)
                {
                    throw new Exception("Block value is larger than modulus n. Encryption aborted.");
                }

                BigInteger encryptedBlock = RSA.RSAEncrypt(blockValue, rsaKey._e, rsaKey._n);
                encryptedBlocks.Add(encryptedBlock);
            }

            // Convert the encrypted blocks to a Base64 string
            List<byte> encryptedBytes = new List<byte>();
            foreach (var encryptedBlock in encryptedBlocks)
            {
                byte[] blockBytes = encryptedBlock.ToByteArray();

                // Ensure consistent byte length for each block
                int byteLength = (int)Math.Ceiling((double)rsaKey._n.GetByteCount());
                if (blockBytes.Length < byteLength)
                {
                    blockBytes = new byte[byteLength - blockBytes.Length].Concat(blockBytes).ToArray();
                }

                encryptedBytes.AddRange(blockBytes);
            }

            return Convert.ToBase64String(encryptedBytes.ToArray());

            /*FOR PRIMES LOWER THAN 1000
             * byte[] encoded = Encoding.UTF8.GetBytes(Message);
            List<byte> encryptedBytes = new List<byte>();

            foreach (byte b in encoded)
            {
                int encryptedBlock = (int)RSAEncrypt(b, _e, _n);
                encryptedBytes.AddRange(BitConverter.GetBytes(encryptedBlock));
            }

            _encryptedMessage = Convert.ToBase64String(encryptedBytes.ToArray());*/
        }

        public static string DecryptWithBlocks(RSAKey rsaKey, string message)
        {
            byte[] encryptedBytes = Convert.FromBase64String(message);
            List<byte> decryptedBytes = new List<byte>();

            // Calculate byte length for each block
            int byteLength = (int)Math.Ceiling((double)rsaKey._n.GetByteCount());

            // Decrypt each block
            for (int i = 0; i < encryptedBytes.Length; i += byteLength)
            {
                byte[] blockToCheck = encryptedBytes.Skip(i).Take(byteLength).ToArray();
                byte[] block = blockToCheck.SkipWhile(x => x == 0).ToArray();
                BigInteger encryptedBlock = new BigInteger(block);
                BigInteger decryptedBlock = RSA.RSAEncrypt(encryptedBlock, rsaKey._d, rsaKey._n);

                byte[] decryptedBlockBytes = decryptedBlock.ToByteArray();

                // Ensure consistent block size handling
                int blockSize = (int)Math.Floor(BigInteger.Log(rsaKey._n, 256)) - 1; // Original block size
                if (decryptedBlockBytes.Length < blockSize)
                {
                    decryptedBlockBytes = new byte[blockSize + 1 - decryptedBlockBytes.Length].Concat(decryptedBlockBytes).ToArray();
                }

                decryptedBytes.AddRange(decryptedBlockBytes.SkipWhile(b => b == 0)); // Skip leading zeros

                /*DIAGNOSIS
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
                END OF DIAGNOSIS*/
            }

            return Encoding.UTF8.GetString(decryptedBytes.ToArray()).TrimEnd('\0');

            /*FOR PRIMES LOWER THAN 1000
             * byte[] encryptedBytes = Convert.FromBase64String(_encryptedMessage);
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
}
