using Aimrank.Domain.RefreshTokens;
using Aimrank.Infrastructure.Domain.Users;
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

            builder.Property(t => t.Jwt).IsRequired();
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.ExpiresAt).IsRequired();
            builder.Property(t => t.IsInvalidated).IsRequired();

            builder.HasOne<UserModel>().WithMany().HasForeignKey("UserId");
        }
    }
}