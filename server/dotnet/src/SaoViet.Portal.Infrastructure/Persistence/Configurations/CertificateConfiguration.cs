using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Enums;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CertificateId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.CertificateDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date")
            .HasDefaultValueSql(DateTimeHelper.Now);

        builder.Property(e => e.Status)
            .HasConversion(v => v!.Value,
                v => CertificateStatus.FromValue(v))
            .HasColumnType("tinyint");

        builder.Property(e => e.ExpiryDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.Property(e => e.ReceivedDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.Property(e => e.Rating)
            .HasColumnType("tinyint")
            .HasDefaultValue(0);

        builder.HasOne(e => e.CourseEnrollment)
            .WithMany(e => e.Certificates)
            .HasForeignKey(e => e.CourseEnrollmentId)
            .HasConstraintName("FK_Certificate_CourseEnrollment")
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.DomainEvents);
    }
}