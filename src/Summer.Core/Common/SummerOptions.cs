using Microsoft.Extensions.DependencyInjection;

namespace Summer.Core.Common
{
    public class SummerOptions
    {
        internal IServiceCollection Services { get; }

        public SummerOptions(IServiceCollection services)
        {
            Services = services;
        }
    }
}
