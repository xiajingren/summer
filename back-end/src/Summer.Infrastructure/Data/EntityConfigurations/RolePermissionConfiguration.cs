using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PermissionCode).HasMaxLength(128).IsRequired();
            builder.HasOne<IdentityRole<int>>()
                .WithMany()
                .HasForeignKey(p => p.RoleId)
                .IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}