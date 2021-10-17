using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Xunit;
using Xunit.Abstractions;

namespace Summer.FunctionalTests
{
    public class UserControllerTest : IClassFixture<WebTestFixture>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public UserControllerTest(WebTestFixture factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ShouldBe_Ok()
        {
            // Arrange
            var registerCommand = new RegisterCommand("testAdmin", "123456");

            // Act
            var response = await _client.PostAsJsonAsync($"/api/user/register", registerCommand);
            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.NotEmpty(result.AccessToken);
        }

        [Fact]
        public async Task Login_ShouldBe_Ok()
        {
            // Arrange
            var loginCommand = new LoginCommand("testAdmin", "123456");

            // Act
            var response = await _client.PostAsJsonAsync($"/api/user/login", loginCommand);
            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.NotEmpty(result.AccessToken);
        }
    }
}