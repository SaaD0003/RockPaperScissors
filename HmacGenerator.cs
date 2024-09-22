using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    public class HmacGenerator
    {
        // Method to generate a random 256-bit key
        public static byte[] GenerateRandomKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32]; // 256 bits = 32 bytes
                rng.GetBytes(key);
                return key;
            }
        }

        // Method to generate HMAC using SHA256
        public static string GenerateHmac(byte[] key, string message)
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
