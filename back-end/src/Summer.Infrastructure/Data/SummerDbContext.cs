using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.Entities;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.Data
{
    public class SummerDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IMediator _mediator;

        public SummerDbContext(DbContextOptions<SummerDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            IdentityEntityConfiguration(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // dispatch events only if save was successful
            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        private void IdentityEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b => { b.ToTable("Users"); });

            modelBuilder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("UserClaims"); });

            modelBuilder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("UserLogins"); });

            modelBuilder.Entity<IdentityUserToken<int>>(b => { b.ToTable("UserTokens"); });

            modelBuilder.Entity<IdentityRole<int>>(b => { b.ToTable("Roles"); });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("RoleClaims"); });

            modelBuilder.Entity<IdentityUserRole<int>>(b => { b.ToTable("UserRoles"); });

            modelBuilder.Entity<RefreshToken>(b =>
            {
                b.ToTable("RefreshTokens");
                b.HasKey(x => x.Id);
                b.Property(x => x.JwtId).HasMaxLength(128).IsRequired();
                b.Property(x => x.Token).HasMaxLength(256).IsRequired();
                b.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .IsRequired();
            });
        }

    }
}