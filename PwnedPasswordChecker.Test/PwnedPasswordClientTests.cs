using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace PwnedPasswordChecker.Test
{
    public class PwnedPasswordClientTests
    {
        [Fact]
        public void PwnedPasswordClient_GetPasswordResults_ParsesToPwnedPasswords()
        {
            // Test Setup
            var mockClient = new Mock<IHttpHandler>();
            mockClient.Setup(m => m.GetAsync(It.Is<string>(s => string.Equals(s, "range/21BD1", StringComparison.OrdinalIgnoreCase))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(KnownPwnedPasswords.GetPwnedPasswordsInApiFormat())});
            PwnedPasswordClient._httpHandler = mockClient.Object;

            var results = new PwnedPasswordClient().GetPasswordResults("21BD1");
            
            Assert.Equal(KnownPwnedPasswords.PwnedPasswords.Values, results);
        }

        [Fact]
        public async Task PwnedPasswordClient_Non200Response_Throws()
        {
            var mockClient = new Mock<IHttpHandler>();
            mockClient.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("") });
            PwnedPasswordClient._httpHandler = mockClient.Object;

            await Assert.ThrowsAsync<ApplicationException>(() => new PwnedPasswordClient().GetPasswordResultsAsync("aaaaa"));
        }

        [Fact]
        public async Task PwnedPasswordClient_NullShaHash_Throws()
        {
            var mockClient = new Mock<IHttpHandler>();
            mockClient.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            PwnedPasswordClient._httpHandler = mockClient.Object;

            await Assert.ThrowsAsync<ArgumentNullException>(() => new PwnedPasswordClient().GetPasswordResultsAsync(null));
        }

        [Fact]
        public async Task PwnedPasswordClient_TooShortShaHash_Throws()
        {
            var mockClient = new Mock<IHttpHandler>();
            mockClient.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            PwnedPasswordClient._httpHandler = mockClient.Object;

            await Assert.ThrowsAsync<ArgumentException>(() => new PwnedPasswordClient().GetPasswordResultsAsync(""));
        }
    }
}
