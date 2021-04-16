using Microsoft.Extensions.DependencyInjection;
using Summer.App.Db;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Summer.App.Contracts.IServices;

namespace Summer.App.Services
{
    public class BaseService : IBaseService
    {
        internal SummerDbContext SummerDbContext { get; }
        internal IServiceProvider ServiceProvider { get; }
        internal ILogger Logger { get; }
        internal IMapper Mapper { get; }

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            SummerDbContext = serviceProvider.GetRequiredService<SummerDbContext>();
            Logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());
            Mapper = serviceProvider.GetRequiredService<IMapper>();
        }
    }
}
