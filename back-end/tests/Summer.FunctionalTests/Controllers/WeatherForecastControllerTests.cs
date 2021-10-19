using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Summer.Application.Responses;
using Summer.WebApi;
using Xunit;
using Xunit.Abstractions;

namespace Summer.FunctionalTests.Controllers
{
    public class WeatherForecastControllerTests : WebTestBase
    {
        public WeatherForecastControllerTests(WebTestFixture factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
        {
        }

        [Fact]
        public async Task Get_ShouldBe_Ok()
        {
            // Arrange
            await Authorization("admin", "123456");

            // Act
            var response = await Client.GetAsync("/WeatherForecast");
            TestOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
