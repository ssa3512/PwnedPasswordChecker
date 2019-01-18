using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PwnedPasswordChecker
{
    public interface IPwnedPasswordClient
    {
        KeyedCollection<string, PwnedPassword> GetPasswordResults(string shaHash);

        Task<KeyedCollection<string, PwnedPassword>> GetPasswordResultsAsync(string shaHash);
    }
}
