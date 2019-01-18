using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PwnedPasswordChecker
{
    internal static class Utils
    {
        internal static string GetHashedPassword(string clearTextPassword)
        {
            using (var sha = SHA1.Create())
            {
                sha.Initialize();
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(clearTextPassword));
                return ByteArrayToString(hash);
            }
        }

        internal static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
