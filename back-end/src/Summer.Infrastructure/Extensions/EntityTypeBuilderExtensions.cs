using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureByConvention<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);

            if (typeof(IAggregateRoot).IsAssignableFrom(builder.Metadata.ClrType))
            {
                builder.Property<int>("TenantId").IsRequired();
            }

            var uniques = builder.Metadata.GetIndexes().Where(x => x.IsUnique).ToList();
            foreach (var index in uniques)
            {
                builder.Metadata.RemoveIndex(index);
                if (index.Name != null)
                {
                    builder.HasIndex(index.Properties.Select(x => x.Name).Append("TenantId").ToArray(), index.Name)
                        .HasDatabaseName(index.GetDatabaseName()).IsUnique();
                }
                else
                {
                    builder.HasIndex(index.Properties.Select(x => x.Name).Append("TenantId").ToArray())
                        .HasDatabaseName(index.GetDatabaseName()).IsUnique();
                }
            }
        }
    }
}