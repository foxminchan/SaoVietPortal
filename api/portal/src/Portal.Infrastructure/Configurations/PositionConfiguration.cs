using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position");

        builder.HasKey(e => e.positionId);
        builder.Property(e => e.positionId)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.positionName)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();
    }
}