using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("PwnedPasswordChecker.Test")]

namespace PwnedPasswordChecker
{
    public class PasswordChecker
    {
        private int _pwnedThreshhold;

        public PasswordChecker() : this(1)
        {

        }

        public PasswordChecker(int pwnedThreshhold)
        {
            _pwnedThreshhold = pwnedThreshhold;
        }

        public PasswordResult CheckPassword(string clearTextPassword)
        {
            return new PasswordResult();
        }

        private string GetHashedPassword(string clearTextPassword)
        {
            using (var sha = SHA1.Create())
            {
                sha.Initialize();
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(clearTextPassword));
                return Encoding.UTF8.GetString(hash);
            }
        }

        
    }
}
