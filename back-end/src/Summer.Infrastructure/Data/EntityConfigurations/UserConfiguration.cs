using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.NormalizedUserName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(256).IsRequired();
            builder.Property(x => x.SecurityStamp).HasMaxLength(64).IsRequired();

            builder.OwnsMany(x => x.Roles, b =>
            {
                b.ToTable("UserRoles");
                b.HasKey(u => new {u.UserId, u.RoleId});
                b.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey(p => p.RoleId)
                    .IsRequired();
                b.WithOwner();
            });

            builder.HasIndex(x => x.NormalizedUserName);
            
            builder.Ignore(x => x.DomainEvents);
        }
    }
}