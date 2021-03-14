using Aimrank.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "users");

            builder.HasKey(u => u.Id);

            builder.Property<string>("_password").HasColumnName("Password");
            builder.Property<string>("_username").HasColumnName("Username");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_steamId").HasColumnName("SteamId").HasMaxLength(17);

            builder.Ignore(u => u.DomainEvents);
        }
    }
}