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

        builder.HasKey(e => e.courseRegistrationId);
        builder.Property(e => e.courseRegistrationId)
            .HasColumnName("Id")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.status)
            .HasColumnName("Status")
            .HasColumnType("nvarchar(15)");

        builder.Property(e => e.registerDate)
            .HasConversion<StringConverter>()
            .HasColumnName("RegisterDate")
            .HasColumnType("date");

        builder.Property(e => e.appointmentDate)
            .HasConversion<StringConverter>()
            .HasColumnName("AppointmentDate")
            .HasColumnType("date");

        builder.Property(e => e.registerFee)
            .HasColumnName("RegisterFee")
            .HasColumnType("float");

        builder.Property(e => e.discountAmount)
            .HasColumnName("DiscountAmount")
            .HasColumnType("decimal(4,2)");

        builder.HasOne(e => e.paymentMethod)
            .WithMany(e => e.courseRegistrations)
            .HasForeignKey(e => e.paymentMethodId)
            .HasConstraintName("FK_CourseRegistration_PaymentMethod")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.courseEnrollment)
            .WithMany(e => e.courseRegistrations)
            .HasForeignKey(e => new { e.studentId, e.classId })
            .HasConstraintName("FK_CourseRegistrations_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);
    }
}