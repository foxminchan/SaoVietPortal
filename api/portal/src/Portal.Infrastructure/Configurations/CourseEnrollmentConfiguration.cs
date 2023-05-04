using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.ToTable("CourseEnrollment");

        builder.HasKey(e => new { studentId = e.StudentId, classId = e.ClassId });

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
    }
}