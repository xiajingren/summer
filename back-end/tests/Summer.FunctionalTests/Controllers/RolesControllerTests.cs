using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Xunit;
using Xunit.Abstractions;

namespace Summer.FunctionalTests.Controllers
{
    public class RolesControllerTests : WebTestBase
    {
        public RolesControllerTests(WebTestFixture factory, ITestOutputHelper testOutputHelper) : base(factory,
            testOutputHelper)
        {
            AuthorizationAsync("admin", "123456").Wait();
        }

        [Fact]
        public async Task CreateRole_ShouldBe_Ok()
        {
            // Arrange
            var createRoleCommand = new CreateRoleCommand("superAdmin");

            // Act
            var response = await Client.PostAsJsonAsync("/api/roles", createRoleCommand);
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<RoleResponse>();
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
        }

        [Fact]
        public async Task UpdateRole_ShouldBe_Ok()
        {
            // Arrange
            var updateRoleCommand = new UpdateRoleCommand(1, "superAdmin-new");

            // Act
            var response = await Client.PutAsJsonAsync("/api/roles/1", updateRoleCommand);
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetRoles_ShouldBe_Ok()
        {
            // Arrange
            
            // Act
            var response = await Client.GetAsync("/api/roles?pageIndex=1&pageSize=10");
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<PaginationResponse<RoleResponse>>();
            Assert.NotNull(result);
            Assert.True(result.Rows.Any());
        }

        [Fact]
        public async Task GetRole_ShouldBe_Ok()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync("/api/roles/1");
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<RoleResponse>();
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
        }

        [Fact]
        public async Task DeleteRole_ShouldBe_Ok()
        {
            // Arrange

            // Act
            var response = await Client.DeleteAsync("/api/roles/1");
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}