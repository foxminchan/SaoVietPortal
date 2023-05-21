using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Enums;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class StudentProgressConfiguration : IEntityTypeConfiguration<StudentProgress>
{
    public void Configure(EntityTypeBuilder<StudentProgress> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new StudentProgressId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.LessonName)
            .HasColumnType("nvarchar(80)")
            .IsRequired();

        builder.Property(e => e.LessonContent)
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.LessonDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.Property(e => e.Status)
            .HasConversion(v => v!.Value,
                v => StudentProgressStatus.FromValue(v))
            .HasColumnType("tinyint");

        builder.Property(e => e.LessonRating)
            .HasColumnType("tinyint")
            .HasDefaultValue(0);

        builder.HasOne(e => e.Staff)
            .WithMany(e => e.StudentProgresses)
            .HasForeignKey(e => e.StaffId)
            .HasConstraintName("FK_StudentProgress_Staff")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CourseEnrollment)
            .WithMany(e => e.StudentProgresses)
            .HasForeignKey(e => e.CourseEnrollmentId)
            .HasConstraintName("FK_StudentProgress_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}