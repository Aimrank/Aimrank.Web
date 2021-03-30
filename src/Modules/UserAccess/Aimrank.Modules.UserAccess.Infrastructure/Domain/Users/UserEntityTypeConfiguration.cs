using Aimrank.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).HasColumnName("Email").IsRequired().HasMaxLength(255);
            builder.Property(u => u.Username).HasColumnName("Username").IsRequired();
            builder.Property(u => u.IsActive).HasColumnName("IsActive").IsRequired();
            builder.Property<string>("_password").HasColumnName("Password").IsRequired();

            builder.OwnsMany<UserToken>("_tokens", b =>
            {
                b.ToTable("UsersTokens", "users");
                b.Property<UserId>("UserId");
                b.HasKey("UserId", "Type");
                b.Property(t => t.Type).HasColumnName("Type").IsRequired();
                b.Property(t => t.Token).HasColumnName("Token").IsRequired();
                b.Property(t => t.ExpiresAt).HasColumnName("ExpiresAt");
                b.WithOwner().HasForeignKey("UserId");
            });

            builder.Ignore(u => u.DomainEvents);
        }
    }
}