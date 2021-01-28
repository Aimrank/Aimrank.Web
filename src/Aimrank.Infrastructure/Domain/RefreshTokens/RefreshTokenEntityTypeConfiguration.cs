using Aimrank.Domain.RefreshTokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Infrastructure.Domain.RefreshTokens
{
    internal class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "aimrank");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Jwt).HasColumnName("Jwt").IsRequired();
            builder.Property(t => t.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(t => t.ExpiresAt).HasColumnName("ExpiresAt").IsRequired();
            builder.Property(t => t.IsInvalidated).HasColumnName("IsInvalidated").IsRequired();

            builder.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId");
        }
    }
}