using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Enums;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new BranchId(v))
            .HasColumnType("char(8)");

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.OwnsOne(e => e.Address, sb => sb.ToJson());

        builder.Property(e => e.Phone)
            .HasColumnType("char(10)");

        builder.Property(e => e.Status)
            .HasConversion(v => v!.Value,
                v => BranchStatus.FromValue(v))
            .HasColumnType("tinyint");

        builder.Ignore(e => e.DomainEvents);
    }
}