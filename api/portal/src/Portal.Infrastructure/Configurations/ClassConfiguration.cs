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

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("char(10)");

        builder.Property(e => e.StartDate)
            .HasConversion<StringConverter>()
            .HasColumnName("StartDate")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.EndDate)
            .HasConversion<StringConverter>()
            .HasColumnName("EndDate")
            .HasColumnType("date");

        builder.Property(e => e.Fee)
            .HasColumnName("Fee")
            .HasColumnType("float");

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.Classes)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_Class_Branch")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Course)
            .WithMany(e => e.Classes)
            .HasForeignKey(e => e.CourseId)
            .HasConstraintName("FK_Class_Course")
            .OnDelete(DeleteBehavior.SetNull);
    }
}