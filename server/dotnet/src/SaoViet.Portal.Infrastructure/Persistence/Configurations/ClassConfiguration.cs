using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new ClassId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.StartDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.EndDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.Property(e => e.Fee)
            .HasColumnType("float")
            .HasDefaultValue(0);

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.Classes)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_Class_Branch")
            .OnDelete(DeleteBehavior.SetNull);

        builder.Ignore(e => e.DomainEvents);
    }
}