using System;
using Microsoft.Extensions.DependencyInjection;

namespace Summer.Core.Common
{
    public static class SummerServiceCollectionExtensions
    {
        public static IServiceCollection AddSummerCore(this IServiceCollection services, Action<SummerOptions> optionsAction = null)
        {
            //todo:
            optionsAction?.Invoke(new SummerOptions(services));
            return services;
        }
    }
}
