using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    public class HmacGenerator
    {
        public static byte[] GenerateRandomKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return key;
            }
        }
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
