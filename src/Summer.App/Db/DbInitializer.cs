using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Summer.App.Db
{
    public class DbInitializer
    {
        internal SummerDbContext SummerDbContext { get; set; }

        public DbInitializer(IServiceProvider serviceProvider)
        {
            SummerDbContext = serviceProvider.GetRequiredService<SummerDbContext>();
        }

        public async Task Initialize()
        {
            await SummerDbContext.Database.MigrateAsync();

            //todo: 初始化种子数据
        }
    }
}
