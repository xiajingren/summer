using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            //builder.HasIndex(x => new {x.TargetId, x.PermissionCode, x.PermissionType}).IsUnique();
            throw new System.NotImplementedException();
        }
    }
}