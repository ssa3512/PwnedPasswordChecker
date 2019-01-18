using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PwnedPasswordChecker.Test
{
    public class UtilsTests
    {
        [Fact]
        public void GetHashedPassword_ReturnsMatchingSHA1Hash()
        {
            var passwordChecker = new PasswordChecker();
            foreach (var item in KnownPwnedPasswords.PwnedPasswords)
            {
                Assert.Equal(item.Value.Hash, Utils.GetHashedPassword(item.Key), ignoreCase: true);
            }
        }
    }
}
