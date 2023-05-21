using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CourseId(v))
            .HasColumnType("varchar(10)");

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.CourseType)
            .WithMany(e => e.Courses)
            .HasForeignKey(e => e.CourseTypeId)
            .HasConstraintName("FK_Course_CourseType")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}