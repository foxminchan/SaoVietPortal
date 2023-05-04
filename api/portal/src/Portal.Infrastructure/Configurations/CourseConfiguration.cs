using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("varchar(10)");

        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");
    }
}