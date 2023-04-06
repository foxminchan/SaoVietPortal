using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.ToTable("CourseEnrollment");

        builder.HasKey(e => new { e.studentId, e.classId });

        builder.HasOne(e => e.student)
            .WithMany(e => e.courseEnrollments)
            .HasForeignKey(e => e.studentId)
            .HasConstraintName("FK_CourseEnrollment_Student")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.@class)
            .WithMany(e => e.courseEnrollments)
            .HasForeignKey(e => e.classId)
            .HasConstraintName("FK_CourseEnrollment_Class")
            .OnDelete(DeleteBehavior.NoAction);
    }
}