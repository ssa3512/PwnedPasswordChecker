using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PwnedPasswordChecker
{
    internal class PwnedPasswordClient : IPwnedPasswordClient
    {
        internal static IHttpHandler _httpHandler;

        static PwnedPasswordClient()
        {
            _httpHandler = new HttpClientHandler("https://api.pwnedpasswords.com/");
        }

        public KeyedCollection<string, PwnedPassword> GetPasswordResults(string shaHash)
        {
            return GetPasswordResultsAsync(shaHash).Result;
        }

        public async Task<KeyedCollection<string, PwnedPassword>> GetPasswordResultsAsync(string shaHash)
        {
            if (shaHash is null)
                throw new ArgumentNullException(nameof(shaHash));

            if (shaHash.Length < 5)
                throw new ArgumentException("shaHash prefix must be at least five characters");

            string prefix = shaHash.Substring(0, 5);
            var response = await _httpHandler.GetAsync($"range/{prefix}");
            string results = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error response received from server: {results}");
            }

            var resultsArray = results.Replace("\n","").Split('\r');

            var pwnedPasswords = new PwnedPasswordList();
            pwnedPasswords.AddRange(resultsArray.Select(result => new PwnedPassword(prefix, result)));
            return pwnedPasswords;
        }
    }
}
