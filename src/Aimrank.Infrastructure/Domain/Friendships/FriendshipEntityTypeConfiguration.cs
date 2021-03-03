using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Infrastructure.Domain.Friendships
{
    internal class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships", "aimrank");

            builder.HasKey(f => f.Members);

            builder.Property<UserId>("_invitingUserId").HasColumnName("InvitingUserId");
            builder.Property<UserId>("_blockingUserId1").HasColumnName("BlockingUserId1");
            builder.Property<UserId>("_blockingUserId2").HasColumnName("BlockingUserId2");
            builder.Property<DateTime>("_createdAt").HasColumnName("CreatedAt");
            builder.Property<bool>("_isAccepted").HasColumnName("IsAccepted");

            builder.HasOne<UserModel>().WithMany().HasForeignKey("_invitingUserId").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<UserModel>().WithMany().HasForeignKey("_blockingUserId1").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<UserModel>().WithMany().HasForeignKey("_blockingUserId2").OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(f => f.Members, b =>
                {
                    b.Property(m => m.UserId1).HasColumnName("Members_UserId1").IsRequired();
                    b.Property(m => m.UserId2).HasColumnName("Members_UserId2").IsRequired();
                    b.HasOne<UserModel>().WithMany().HasForeignKey(m => m.UserId1).OnDelete(DeleteBehavior.Cascade);
                    b.HasOne<UserModel>().WithMany().HasForeignKey(m => m.UserId2).OnDelete(DeleteBehavior.Cascade);
                })
                .Navigation(f => f.Members).IsRequired();

            builder.Ignore(f => f.DomainEvents);
        }
    }
}