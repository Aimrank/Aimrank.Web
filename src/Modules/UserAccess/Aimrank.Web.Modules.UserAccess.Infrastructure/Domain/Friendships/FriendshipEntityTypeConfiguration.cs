using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Friendships
{
    internal class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships");

            builder.HasKey(f => new {f.User1Id, f.User2Id});

            builder.Property(f => f.User1Id).HasColumnName("User1Id");
            builder.Property(f => f.User2Id).HasColumnName("User2Id");
            builder.Property<UserId>("_invitingUserId").HasColumnName("InvitingUserId");
            builder.Property<UserId>("_blockingUserId1").HasColumnName("BlockingUserId1");
            builder.Property<UserId>("_blockingUserId2").HasColumnName("BlockingUserId2");
            builder.Property<DateTime>("_createdAt").HasColumnName("CreatedAt");
            builder.Property<bool>("_isAccepted").HasColumnName("IsAccepted");

            builder.HasOne<User>().WithMany().HasForeignKey(f => f.User1Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>().WithMany().HasForeignKey(f => f.User2Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>().WithMany().HasForeignKey("_invitingUserId").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>().WithMany().HasForeignKey("_blockingUserId1").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>().WithMany().HasForeignKey("_blockingUserId2").OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(f => f.Members);
            builder.Ignore(f => f.DomainEvents);
        }
    }
}