using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PermissionCode).HasMaxLength(128).IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}