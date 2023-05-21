using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Infrastructure.Outbox;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class OutboxConfiguration : IEntityTypeConfiguration<OutboxEntity>
{
    public void Configure(EntityTypeBuilder<OutboxEntity> builder)
    {
        builder.ToTable("Outbox")
            .HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Type)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Data)
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.OccurredOn)
            .HasColumnType("datetime");

        builder.Property(e => e.ProcessedDate)
            .HasColumnType("datetime");

        builder.Property(e => e.Error)
            .HasColumnType("nvarchar(max)");
    }
}