using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("PwnedPasswordChecker.Test")]

namespace PwnedPasswordChecker
{
    public class PasswordChecker
    {
        internal static IPwnedPasswordClient _pwnedPasswordClient = new PwnedPasswordClient();
        private int _pwnedThreshhold;

        public PasswordChecker() : this(1)
        {

        }

        public PasswordChecker(int pwnedThreshhold)
        {
            _pwnedThreshhold = pwnedThreshhold;
        }

        public async Task<PasswordResult> CheckHashedPasswordAsync(byte[] hashedPassword)
        {
            if (hashedPassword is null)
                throw new ArgumentNullException(nameof(hashedPassword));

            string shaHash = Utils.ByteArrayToString(hashedPassword);
            return await CheckHashedPasswordAsync(shaHash);
        }

        public PasswordResult CheckHashedPassword(byte[] hashedPassword)
        {
            if (hashedPassword is null)
                throw new ArgumentNullException(nameof(hashedPassword));

            return CheckHashedPasswordAsync(hashedPassword).Result;
        }

        public async Task<PasswordResult> CheckHashedPasswordAsync(string hashedPassword)
        {
            if (hashedPassword is null)
                throw new ArgumentNullException(nameof(hashedPassword));

            hashedPassword = hashedPassword.ToUpper();
            var regex = new Regex("[0-9A-F]{40}");
            if (!regex.IsMatch(hashedPassword))
            {
                throw new InvalidOperationException("Hashed password was not a valid SHA-1 hash");
            }

            var results = await _pwnedPasswordClient.GetPasswordResultsAsync(hashedPassword);

            var passwordResult = new PasswordResult();
            passwordResult.TimesPwned = 0;
            passwordResult.PasswordHash = hashedPassword;
            passwordResult.IsPwned = false;
            passwordResult.PwnedLevel = PwnedLevel.Safe;

            if (results.Contains(hashedPassword))
            {
                passwordResult.IsPwned = true;
                passwordResult.TimesPwned = results[hashedPassword].TimesPwned;
                passwordResult.PwnedLevel = passwordResult.TimesPwned >= _pwnedThreshhold ? PwnedLevel.Pwned : PwnedLevel.Warn;
            }

            return passwordResult;
        }

        public PasswordResult CheckHashedPassword(string hashedPassword)
        {
            if (hashedPassword is null)
                throw new ArgumentNullException(nameof(hashedPassword));

            return CheckHashedPasswordAsync(hashedPassword).Result;
        }

        public async Task<PasswordResult> CheckClearTextPasswordAsync(string clearTextPassword)
        {
            var hashedPassword = Utils.GetHashedPassword(clearTextPassword);

            return await CheckHashedPasswordAsync(hashedPassword);
        }

        public PasswordResult CheckClearTextPassword(string clearTextPassword)
        {
            return CheckClearTextPasswordAsync(clearTextPassword).Result;
        }

               
    }
}
