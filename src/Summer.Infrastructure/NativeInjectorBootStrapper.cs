using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Summer.Infrastructure.Identity;
using Summer.Infrastructure.IdentityServer;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure
{
    public static class NativeInjectorBootStrapper
    {
        private static readonly string MigrationsAssembly =
            typeof(NativeInjectorBootStrapper).GetTypeInfo().Assembly.GetName().Name;

        public static void RegisterServices(IServiceCollection services)
        {
            AddIdentity(services);
            AddIdentityServer(services);
            AddDbContextSeed(services);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            DbContextSeed(app);
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("summer.db",
                    sqlOptions => { sqlOptions.MigrationsAssembly(MigrationsAssembly); }));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlite("summer.db",
                        sqlOptions => { sqlOptions.MigrationsAssembly(MigrationsAssembly); });
                }).AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlite("summer.db",
                        sqlOptions => { sqlOptions.MigrationsAssembly(MigrationsAssembly); });
                });
        }

        private static void AddDbContextSeed(IServiceCollection services)
        {
            services.AddScoped<IDbContextSeed, ConfigurationDbContextSeed>();
            services.AddScoped<IDbContextSeed, ApplicationDbContextSeed>();
        }

        private static void DbContextSeed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var seeds = scope.ServiceProvider.GetServices<IDbContextSeed>();
            foreach (var seed in seeds)
            {
                seed.SeedAsync().Wait();
            }
        }
    }
}