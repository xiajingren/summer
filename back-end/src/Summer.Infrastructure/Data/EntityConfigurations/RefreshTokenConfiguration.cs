﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Summer.Domain.Entities;

namespace Summer.Infrastructure.Data.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.JwtId).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(256).IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}