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
            builder.ToTable("friendships");

            builder.HasKey(f => new {f.User1Id, f.User2Id});

            builder.Property(f => f.User1Id).HasColumnName("user_1_id");
            builder.Property(f => f.User2Id).HasColumnName("user_2_id");
            builder.Property<UserId>("_invitingUserId").HasColumnName("inviting_user_id");
            builder.Property<UserId>("_blockingUserId1").HasColumnName("blocking_user_id_1");
            builder.Property<UserId>("_blockingUserId2").HasColumnName("blocking_user_id_2");
            builder.Property<DateTime>("_createdAt").HasColumnName("created_at");
            builder.Property<bool>("_isAccepted").HasColumnName("is_accepted");

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