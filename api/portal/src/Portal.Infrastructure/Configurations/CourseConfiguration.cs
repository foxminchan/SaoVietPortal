using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(e => e.courseId);

        builder.Property(e => e.courseId)
            .HasColumnName("Id")
            .HasColumnType("varchar(10)");

        builder.Property(e => e.courseName)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");
    }
}