using System;
using System.Collections.Generic;
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
            _httpHandler = new HttpClientHandler();
            _httpHandler.BaseAddress = new Uri("https://api.pwnedpasswords.com/");
        }

        public IEnumerable<PwnedPassword> GetPasswordResults(string shaHash)
        {
            return GetPasswordResultsAsync(shaHash).Result;
        }

        public async Task<IEnumerable<PwnedPassword>> GetPasswordResultsAsync(string shaHash)
        {
            string prefix = shaHash.ToUpper().Substring(0, 5);

            var response = await _httpHandler.GetAsync($"range/{prefix}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error response received from server");
            }

            string results = await response.Content.ReadAsStringAsync();
            var resultsArray = results.Replace("\n","").Split('\r');
            return resultsArray.Select(result => new PwnedPassword(prefix, result));
        }
    }
}
