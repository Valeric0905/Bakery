using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BakeryWeb.Helpers
{

    public static class HashHelper
    {
        /// <summary>
        /// Generate an MD5 hash for a given input string.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>MD5 hash as a hexadecimal string.</returns>
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Convert byte to hexadecimal
                }

                return sb.ToString();
            }
        }
    }
}
