using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Enums;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CourseEnrollmentId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.EnrollmentDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date")
            .HasDefaultValueSql(DateTimeHelper.Now);

        builder.Property(e => e.StudyStatus)
            .HasConversion(v => v!.Value,
                v => StudyStatus.FromValue(v))
            .HasColumnType("tinyint");

        builder.HasOne(e => e.Student)
            .WithMany(e => e.CourseEnrollments)
            .HasForeignKey(e => e.StudentId)
            .HasConstraintName("FK_CourseEnrollment_Student")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Class)
            .WithMany(e => e.CourseEnrollments)
            .HasForeignKey(e => e.ClassId)
            .HasConstraintName("FK_CourseEnrollment_Class")
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(e => e.DomainEvents);
    }
}