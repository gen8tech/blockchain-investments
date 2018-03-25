using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Gen8.Ledger.Api
{
    public class AboutControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        string testMessage;
        public AboutControllerTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();

        }

        [Fact]
        public async Task Echo_Always_ReturnsTestAPI()
        {
            // Arrange
            testMessage = "TestAPI";
            string uri = string.Format("/api/about/echo?message={0}", testMessage);

            // Act
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(testMessage,
                responseString);
        }
    }
}
