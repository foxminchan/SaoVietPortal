using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class CourseRegistrationConfiguration : IEntityTypeConfiguration<CourseRegistration>
{
    public void Configure(EntityTypeBuilder<CourseRegistration> builder)
    {
        builder.ToTable("CourseRegistration");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.Status)
            .HasColumnName("Status")
            .HasColumnType("nvarchar(15)");

        builder.Property(e => e.RegisterDate)
            .HasConversion<StringConverter>()
            .HasColumnName("RegisterDate")
            .HasColumnType("date");

        builder.Property(e => e.AppointmentDate)
            .HasConversion<StringConverter>()
            .HasColumnName("AppointmentDate")
            .HasColumnType("date");

        builder.Property(e => e.Fee)
            .HasColumnName("RegisterFee")
            .HasColumnType("float");

        builder.Property(e => e.DiscountAmount)
            .HasColumnName("DiscountAmount")
            .HasColumnType("decimal(4,2)");

        builder.HasOne(e => e.PaymentMethod)
            .WithMany(e => e.CourseRegistrations)
            .HasForeignKey(e => e.PaymentMethodId)
            .HasConstraintName("FK_CourseRegistration_PaymentMethod")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.CourseEnrollment)
            .WithMany(e => e.CourseRegistrations)
            .HasForeignKey(e => new { studentId = e.StudentId, classId = e.ClassId })
            .HasConstraintName("FK_CourseRegistrations_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);
    }
}