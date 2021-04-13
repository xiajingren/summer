using Microsoft.EntityFrameworkCore;
using Summer.App.Entities;

namespace Summer.App.Db
{
    internal class SummerDbContext : DbContext
    {
        public SummerDbContext(DbContextOptions<SummerDbContext> options)
            : base(options)
        {

        }

        public DbSet<SysUser> SysUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SysUser>(b =>
            {
                b.ToTable("SysUsers");
            });
        }

    }
}
