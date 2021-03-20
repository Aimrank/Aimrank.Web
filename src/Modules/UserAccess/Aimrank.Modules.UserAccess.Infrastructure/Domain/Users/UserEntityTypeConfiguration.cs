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
            builder.Property<string>("_password").HasColumnName("Password").IsRequired();

            builder.Ignore(u => u.DomainEvents);
        }
    }
}