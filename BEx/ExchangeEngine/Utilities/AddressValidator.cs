using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;

namespace BEx.ExchangeEngine.Utilities
{
    internal class AddressValidator
    {
        // https://en.bitcoin.it/wiki/Base58Check_encoding#Creating_a_Base58Check_string
        // http://bitcoin.stackexchange.com/questions/32353/how-do-i-check-the-checksum-of-a-bitcoin-address

        private static readonly BigInteger bigInt58 = new BigInteger("58");
        private static readonly string validChars = @"123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static bool IsValid(string address)
        {
            return Base58CheckToByteArray(address) != null;
        }

        private static byte[] AddressToByteArray(string address)
        {
            var target = new BigInteger("0");

            foreach (var character in address)
            {
                if (validChars.IndexOf(character) != -1)
                {
                    target = target.Multiply(bigInt58);
                    target = target.Add(new BigInteger(validChars.IndexOf(character).ToString()));
                }
                else
                {
                    return null;
                }
            }

            var bytes = target.ToByteArrayUnsigned();

            foreach (var character in address)
            {
                if (character != '1')
                {
                    break;
                }

                var final = new byte[bytes.Length + 1];
                Array.Copy(bytes, 0, final, 1, bytes.Length);
                bytes = final;
            }

            return bytes;
        }

        private static byte[] Base58CheckToByteArray(string address)
        {
            var bytes = AddressToByteArray(address);

            if (bytes == null || bytes.Length < 4)
            {
                return null;
            }

            var adjustedLength = bytes.Length - 4;
            var checksum = new byte[32];

            var sha256 = new Sha256Digest();

            sha256.BlockUpdate(bytes, 0, adjustedLength);
            sha256.DoFinal(checksum, 0);
            sha256.BlockUpdate(checksum, 0, 32);
            sha256.DoFinal(checksum, 0);

            for (var i = 0; i < 4; i++)
            {
                if (checksum[i] != bytes[adjustedLength + i]) return null;
            }
            return bytes;
        }
    }
}