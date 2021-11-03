using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Summer.Domain.Entities;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.MasterData
{
    public class MasterDbContext : BaseDbContext
    {
        private readonly IConfiguration _configuration;

        protected override string ConnectionString =>
            _configuration.GetConnectionString("Master") ?? _configuration.GetConnectionString("Default");

        public MasterDbContext(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(b =>
            {
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}