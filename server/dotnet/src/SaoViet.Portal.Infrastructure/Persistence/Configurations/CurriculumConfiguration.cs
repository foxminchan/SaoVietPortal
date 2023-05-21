using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CurriculumConfiguration : IEntityTypeConfiguration<Curriculum>
{
    public void Configure(EntityTypeBuilder<Curriculum> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CurriculumId(v))
            .HasColumnType("smallint")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.StartTime)
            .HasConversion<TimeOnlyConverter, TimeOnlyComparer>()
            .HasColumnType("time");

        builder.Property(e => e.EndTime)
            .HasConversion<TimeOnlyConverter, TimeOnlyComparer>()
            .HasColumnType("time");

        builder.HasOne(e => e.Class)
            .WithMany(e => e.Curricula)
            .HasForeignKey(e => e.ClassId)
            .HasConstraintName("FK_Curriculum_Class")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Course)
            .WithMany(e => e.Curricula)
            .HasForeignKey(e => e.CourseId)
            .HasConstraintName("FK_Curriculum_Course")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}