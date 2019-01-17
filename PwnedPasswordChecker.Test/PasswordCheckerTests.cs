using System;
using System.Linq;
using PwnedPasswordChecker;
using Xunit;
using Moq;

namespace PwnedPasswordChecker.Test
{
    public class PasswordCheckerTests
    {
        [Fact]
        public void TestPasswordIsPwned()
        {
            var result = new PwnedPasswordClient().GetPasswordResults("aaaaa");
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}
