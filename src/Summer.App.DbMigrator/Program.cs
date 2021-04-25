using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Summer.App.Contracts.Core;
using Summer.App.Db;

namespace Summer.App.DbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var appOptions = hostContext.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
                    services.AddSingleton(appOptions);

                    services.AddHostedService<DbMigrateService>()
                        .AddSummer()
                        .AddSingleton<DbInitializer>();
                });
    }
}