using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new StudentId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.UuidSequential);

        builder.Property(e => e.Fullname)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Gender)
            .HasColumnType("bit")
            .HasDefaultValue(false)
            .IsRequired();

        builder.OwnsOne(e => e.Address,
            sb => sb.ToJson());

        builder.Property(e => e.DateOfBirth)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.OwnsOne(e => e.PlaceOfBirth,
            sb => sb.ToJson());

        builder.Property(e => e.Occupation)
            .HasColumnType("nvarchar(80)");

        builder.OwnsMany(e => e.SocialNetwork,
            sb => sb.ToJson());

        builder.Ignore(e => e.DomainEvents);
    }
}