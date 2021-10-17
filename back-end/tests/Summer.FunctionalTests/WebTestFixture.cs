using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Summer.Infrastructure.Identity;
using Summer.WebApi;

namespace Summer.FunctionalTests
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(UserDbContext));
                services.AddDbContext<UserDbContext>(options => { options.UseInMemoryDatabase("TestDB"); });
            });
        }
    }
}