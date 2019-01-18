using System;
using System.Linq;
using PwnedPasswordChecker;
using Xunit;
using Moq;
using System.Net.Http;
using System.Net;

namespace PwnedPasswordChecker.Test
{
    public class PasswordCheckerTests
    {
        // Test Setup
        public PasswordCheckerTests()
        {
            var mockClient = new Mock<IPwnedPasswordClient>();
            mockClient.Setup(m => m.GetPasswordResultsAsync(It.Is<string>(s => s.StartsWith("21BD1"))))
                .ReturnsAsync(new PwnedPasswordList(KnownPwnedPasswords.PwnedPasswords.Values));
            PasswordChecker._pwnedPasswordClient = mockClient.Object;
        }

        [Fact]
        public void PasswordChecker_CheckHashedPassword_ThrowsOnNullBytes()
        {
            Assert.Throws<ArgumentNullException>(() => new PasswordChecker().CheckHashedPassword((byte[])null));
        }

        [Fact]
        public void PasswordChecker_CheckHashedPassword_ThrowsOnNullString()
        {
            Assert.Throws<ArgumentNullException>(() => new PasswordChecker().CheckHashedPassword((string)null));
        }

        [Fact]
        public void PasswordChecker_CheckHashedPasswordAsync_ThrowsOnNullBytes()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => new PasswordChecker().CheckHashedPasswordAsync((byte[])null));
        }

        [Fact]
        public void PasswordChecker_CheckHashedPasswordAsync_ThrowsOnNullString()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => new PasswordChecker().CheckHashedPasswordAsync((string)null));
        }


        [Fact]
        public void TestPasswordIsPwned()
        {
            var result = new PasswordChecker().CheckClearTextPassword("lauragpe");
            Assert.NotNull(result);
            Assert.True(result.IsPwned);
            Assert.Equal(PwnedLevel.Pwned, result.PwnedLevel);
            Assert.Equal(1, result.TimesPwned);
            Assert.Equal("21BD10018A45C4D1DEF81644B54AB7F969B88D65", result.PasswordHash);
        }

        
    }
}
