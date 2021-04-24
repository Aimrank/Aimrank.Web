using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property<string>("_password").HasColumnName("password").IsRequired();

            builder.OwnsMany<UserToken>("_tokens", b =>
            {
                b.ToTable("users_tokens");
                b.Property<UserId>("UserId");
                b.HasKey("UserId", "Type");
                b.Property(t => t.Type).IsRequired();
                b.Property(t => t.Token).IsRequired();
                b.Property(t => t.ExpiresAt).HasColumnName("expires_at");
                b.WithOwner().HasForeignKey("UserId");
            });

            builder.Ignore(u => u.DomainEvents);
        }
    }
}