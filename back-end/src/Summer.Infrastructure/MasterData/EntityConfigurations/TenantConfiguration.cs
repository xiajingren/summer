using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;
using Summer.Infrastructure.Extensions;

namespace Summer.Infrastructure.MasterData.EntityConfigurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            
            builder.Property(x => x.Code).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.ConnectionString).HasMaxLength(256);
            builder.Property(x => x.Host).HasMaxLength(128);

            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => x.Host).IsUnique();

            builder.ConfigureByConvention(false);
        }
    }
}