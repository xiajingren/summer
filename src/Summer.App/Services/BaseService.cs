using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Summer.App.Db;

namespace Summer.App.Services
{
    public class BaseService
    {
        internal SummerDbContext SummerDbContext { get; set; }

        public BaseService(IServiceProvider serviceProvider)
        {
            SummerDbContext = serviceProvider.GetRequiredService<SummerDbContext>();
        }
    }
}
