using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing.Inbox
{
    internal class InboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("inbox_messages");
            builder.HasKey(x => x.Id);
            builder.Property<DateTime>("OccurredOn").HasColumnName("occurred_on");
            builder.Property<DateTime?>("ProcessedDate").HasColumnName("processed_date");
            builder.Property<string>("Type").IsRequired().HasMaxLength(255);
            builder.Property<string>("Data").IsRequired();
        }
    }
}