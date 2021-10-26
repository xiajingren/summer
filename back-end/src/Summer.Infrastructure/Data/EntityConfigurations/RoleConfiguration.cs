using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.NormalizedName).HasMaxLength(64).IsRequired();

            builder.HasIndex(x => x.NormalizedName);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}