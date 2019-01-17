using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PwnedPasswordChecker
{
    public interface IPwnedPasswordClient
    {
        IEnumerable<PwnedPassword> GetPasswordResults(string shaHash);

        Task<IEnumerable<PwnedPassword>> GetPasswordResultsAsync(string shaHash);
    }
}
