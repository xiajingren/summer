using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.PermissionCode).HasMaxLength(64).IsRequired();
            
            builder.HasIndex(x => new { x.TargetId, x.PermissionType, x.PermissionCode }).IsUnique();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}