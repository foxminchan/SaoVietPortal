using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Class");

        builder.HasKey(e => e.classId);

        builder.Property(e => e.classId)
            .HasColumnName("Id")
            .HasColumnType("char(10)");

        builder.Property(e => e.startDate)
            .HasConversion<StringConverter>()
            .HasColumnName("StartDate")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.endDate)
            .HasConversion<StringConverter>()
            .HasColumnName("EndDate")
            .HasColumnType("date");

        builder.Property(e => e.fee)
            .HasColumnName("Fee")
            .HasColumnType("float");

        builder.HasOne(e => e.branch)
            .WithMany(e => e.classes)
            .HasForeignKey(e => e.branchId)
            .HasConstraintName("FK_Class_Branch")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.course)
            .WithMany(e => e.classes)
            .HasForeignKey(e => e.courseId)
            .HasConstraintName("FK_Class_Course")
            .OnDelete(DeleteBehavior.SetNull);
    }
}