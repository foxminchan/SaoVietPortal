using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class StudentProgressConfiguration : IEntityTypeConfiguration<StudentProgress>
{
    public void Configure(EntityTypeBuilder<StudentProgress> builder)
    {
        builder.ToTable("StudentProgress");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.LessonName)
            .HasColumnName("LessonName")
            .HasColumnType("nvarchar(80)")
            .IsRequired();

        builder.Property(e => e.LessonContent)
            .HasColumnName("LessonContent")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.LessonDate)
            .HasConversion<StringConverter>()
            .HasColumnName("LessonDate")
            .HasColumnType("date");

        builder.Property(e => e.Status)
            .HasColumnName("Status")
            .HasColumnType("nvarchar(15)");

        builder.Property(e => e.LessonRating)
            .HasColumnName("LessonRating")
            .HasColumnType("float");

        builder.HasOne(e => e.Staff)
            .WithMany(e => e.StudentProgresses)
            .HasForeignKey(e => e.StaffId)
            .HasConstraintName("FK_StudentProgress_Staff")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.CourseEnrollment)
            .WithMany(e => e.StudentProgresses)
            .HasForeignKey(e => new { studentId = e.StudentId, classId = e.ClassId })
            .HasConstraintName("FK_StudentProgress_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);
    }
}