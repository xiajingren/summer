using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;
using Summer.Infrastructure.Extensions;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            
            builder.Property(x => x.JwtId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(128).IsRequired();
            
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.HasIndex(x => x.JwtId).IsUnique();
            builder.HasIndex(x => x.Token).IsUnique();

            builder.ConfigureByConvention();
        }
    }
}