using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Summer.Application.Apis.Auth;
using Summer.Application.Apis.Auth.Login;
using Xunit;
using Xunit.Abstractions;

namespace Summer.FunctionalTests
{
    public class WebTestBase : IClassFixture<WebTestFixture>
    {
        public ITestOutputHelper TestOutputHelper { get; }
        public HttpClient Client { get; }

        public WebTestBase(WebTestFixture factory, ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
            Client = factory.CreateClient();
        }

        public async Task AuthorizationAsync(string userName, string password)
        {
            var loginCommand = new LoginCommand(userName, password);

            var response = await Client.PostAsJsonAsync($"/api/user/login", loginCommand);
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {result.AccessToken}");
        }
    }
}