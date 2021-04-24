using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages");
            builder.HasKey(x => x.Id);
            builder.Property<DateTime>("OccurredOn").HasColumnName("occurred_on");
            builder.Property<DateTime?>("ProcessedDate").HasColumnName("processed_date");
            builder.Property<string>("Type").IsRequired().HasMaxLength(255);
            builder.Property<string>("Data").IsRequired();
        }
    }
}