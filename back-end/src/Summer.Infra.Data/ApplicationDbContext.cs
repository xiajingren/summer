using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.Entities;

namespace Summer.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b => { b.ToTable("ApplicationUsers"); });

            builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("ApplicationUserClaims"); });

            builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("ApplicationUserLogins"); });

            builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("ApplicationUserTokens"); });

            builder.Entity<IdentityRole>(b => { b.ToTable("ApplicationRoles"); });

            builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("ApplicationRoleClaims"); });

            builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("ApplicationUserRoles"); });
        }
    }
}