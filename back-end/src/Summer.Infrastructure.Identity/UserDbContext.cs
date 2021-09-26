using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Summer.Infrastructure.Identity.Entities;

namespace Summer.Infrastructure.Identity
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b => { b.ToTable("AppUsers"); });

            builder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("AppUserClaims"); });

            builder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("AppUserLogins"); });

            builder.Entity<IdentityUserToken<int>>(b => { b.ToTable("AppUserTokens"); });

            builder.Entity<IdentityRole<int>>(b => { b.ToTable("AppRoles"); });

            builder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("AppRoleClaims"); });

            builder.Entity<IdentityUserRole<int>>(b => { b.ToTable("AppUserRoles"); });

            builder.Entity<RefreshToken>(b => { b.ToTable("AppRefreshTokens"); });
        }
    }
}