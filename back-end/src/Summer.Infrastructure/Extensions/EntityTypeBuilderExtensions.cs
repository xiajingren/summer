using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureByConvention<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.Property<int>("TenantId").IsRequired();
            
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}