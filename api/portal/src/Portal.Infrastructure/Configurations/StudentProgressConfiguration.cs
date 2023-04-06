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

        builder.HasKey(e => e.progressId);
        builder.Property(e => e.progressId)
            .HasColumnName("Id")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.lessonName)
            .HasColumnName("LessonName")
            .HasColumnType("nvarchar(80)")
            .IsRequired();

        builder.Property(e => e.lessonContent)
            .HasColumnName("LessonContent")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.lessonDate)
            .HasConversion<StringConverter>()
            .HasColumnName("LessonDate")
            .HasColumnType("date");

        builder.Property(e => e.progressStatus)
            .HasColumnName("ProgressStatus")
            .HasColumnType("nvarchar(15)");

        builder.Property(e => e.lessonRating)
            .HasColumnName("LessonRating")
            .HasColumnType("float");

        builder.HasOne(e => e.staff)
            .WithMany(e => e.studentProgresses)
            .HasForeignKey(e => e.staffId)
            .HasConstraintName("FK_StudentProgress_Staff")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.courseEnrollment)
            .WithMany(e => e.studentProgresses)
            .HasForeignKey(e => new { e.studentId, e.classId })
            .HasConstraintName("FK_StudentProgress_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);
    }
}