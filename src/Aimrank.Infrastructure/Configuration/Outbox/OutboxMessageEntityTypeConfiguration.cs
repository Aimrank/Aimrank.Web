using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Infrastructure.Configuration.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "aimrank");
            builder.HasKey(x => x.Id);
            builder.Property<DateTime>("OccurredAt");
            builder.Property<string>("Type").IsRequired().HasMaxLength(255);
            builder.Property<string>("Data").IsRequired();
        }
    }
}