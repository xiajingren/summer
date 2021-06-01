using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Summer.App.Contracts.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSummer(this IServiceCollection services)
        {
            // todo: 
            var httpContextAccessor = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
            AppGlobal.Configure(httpContextAccessor);

            // var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
            //         a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IAppStartup))))
            //     .ToArray();

            var types = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Summer.App.dll"))
                .GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IAppStartup))).ToArray();

            foreach (var type in types)
            {
                var appStartup = Activator.CreateInstance(type) as IAppStartup;
                services = appStartup?.ConfigureServices(services);

                appStartup?.Start(services.BuildServiceProvider());
            }

            return services;
        }
    }
}