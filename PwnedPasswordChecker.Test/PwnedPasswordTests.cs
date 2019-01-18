using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PwnedPasswordChecker.Test
{
    public class PwnedPasswordTests
    {
        [Fact]
        public void PwnedPassword_Equals_NullOther_ReturnsFalse()
        {
            Assert.False(new PwnedPassword(null, null).Equals(null));
        }
    }
}
