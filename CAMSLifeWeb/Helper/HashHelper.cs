using System;
using System.Security.Cryptography;
using System.Text;

namespace Caliph.Library.Helper
{
    /// <summary>
    /// Generate Md5 and SHA hashes in C#.NET.
    /// Code Link Ref: https://gist.github.com/rmacfie/828054
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Calculates the MD5 hash for the given string.
        /// </summary>
        /// <returns>A 32 char long MD5 hash.</returns>
        public static string GetHashMd5(this string input)
        {
            return ComputeHash(input, new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// Calculates the SHA-1 hash for the given string.
        /// </summary>
        /// <returns>A 40 char long SHA-1 hash.</returns>
        public static string GetHashSha1(this string input)
        {
            return ComputeHash(input, new SHA1Managed());
        }

        /// <summary>
        /// Calculates the SHA-256 hash for the given string.
        /// </summary>
        /// <returns>A 64 char long SHA-256 hash.</returns>
        public static string GetHashSha256(this string input)
        {
            return ComputeHash(input, new SHA256Managed());
        }

        /// <summary>
        /// Calculates the SHA-384 hash for the given string.
        /// </summary>
        /// <returns>A 96 char long SHA-384 hash.</returns>
        public static string GetHashSha384(this string input)
        {
            return ComputeHash(input, new SHA384Managed());
        }

        /// <summary>
        /// Calculates the SHA-512 hash for the given string.
        /// </summary>
        /// <returns>A 128 char long SHA-512 hash.</returns>
        public static string GetHashSha512(this string input)
        {
            return ComputeHash(input, new SHA512Managed());
        }

        /// <summary>
        /// Compute Hash
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hashProvider"></param>
        /// <returns></returns>
        private static string ComputeHash(string input, HashAlgorithm hashProvider)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (hashProvider == null)
            {
                throw new ArgumentNullException("hashProvider");
            }

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = hashProvider.ComputeHash(inputBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            return hash;
        }
        
    }
}
