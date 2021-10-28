using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;
using Summer.Infrastructure.Extensions;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            
            builder.Property(x => x.PermissionCode).HasMaxLength(64).IsRequired();
            
            builder.HasIndex(x => new { x.TargetId, x.PermissionType, x.PermissionCode }).IsUnique();

            builder.ConfigureByConvention();
        }
    }
}