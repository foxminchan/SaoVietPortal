using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Enums;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CourseRegistrationConfiguration : IEntityTypeConfiguration<CourseRegistration>
{
    public void Configure(EntityTypeBuilder<CourseRegistration> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CourseRegistrationId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.Status)
            .HasConversion(v => v!.Value,
                v => CourseRegisterStatus.FromValue(v))
            .HasColumnType("tinyint");

        builder.Property(e => e.RegisterDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.Property(e => e.AppointmentDate)
            .HasColumnType("datetime");

        builder.Property(e => e.Fee)
            .HasColumnType("money");

        builder.Property(e => e.DiscountAmount)
            .HasColumnType("tinyint");

        builder.HasOne(e => e.CourseEnrollment)
            .WithMany(e => e.CourseRegistrations)
            .HasForeignKey(e => e.CourseEnrollmentId)
            .HasConstraintName("FK_CourseRegistrations_CourseEnrollment")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PaymentMethod)
            .WithMany(e => e.CourseRegistrations)
            .HasForeignKey(e => e.PaymentMethodId)
            .HasConstraintName("FK_CourseRegistrations_PaymentMethod")
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.DomainEvents);
    }
}