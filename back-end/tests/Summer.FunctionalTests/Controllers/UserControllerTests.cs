using System.Net.Http.Json;
using System.Threading.Tasks;
using Summer.Application.Requests.Commands;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Xunit;
using Xunit.Abstractions;

namespace Summer.FunctionalTests.Controllers
{
    public class UserControllerTests : WebTestBase
    {
        public UserControllerTests(WebTestFixture factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
        {
        }

        [Fact]
        public async Task Register_ShouldBe_Ok()
        {
            // Arrange
            var registerCommand = new RegisterCommand("tester", "123456");

            // Act
            var response = await Client.PostAsJsonAsync("/api/user/register", registerCommand);
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

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
            var loginCommand = new LoginCommand("tester", "123456");

            // Act
            var response = await Client.PostAsJsonAsync("/api/user/login", loginCommand);
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.NotEmpty(result.AccessToken);
        }

        [Fact]
        public async Task RefreshToken_ShouldBe_Ok()
        {
            // Arrange
            var refreshTokenCommand = new RefreshTokenCommand("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJmNjZiNWE2NmVmODU0ZTgxYTY1MjMyZDkwNjhkMjdjZSIsInN1YiI6IjIiLCJuYmYiOjE2MzQ2MjQ1NzksImV4cCI6MTYzNDYyNTE3OSwiaWF0IjoxNjM0NjI0NTc5fQ.fOPCZ-h2nHO7KIL5xFyHYZWG270i_odCVFpur_YyEI4", "bTpEfu2KB3hoGwAupftn5uX98U9SZvPq9q8um0pYhO4=");

            // Act
            var response = await Client.PostAsJsonAsync("/api/user/refresh-token", refreshTokenCommand);
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.NotEmpty(result.AccessToken);
        }

        [Fact]
        public async Task GetUserProfile_ShouldBe_Ok()
        {
            // Arrange
            await Authorization("admin", "123456");

            // Act
            var response = await Client.GetAsync("/api/user/profile");
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<UserProfileResponse>();
            Assert.NotNull(result);
            Assert.True(result.IsAuthenticated);
        }

    }
}