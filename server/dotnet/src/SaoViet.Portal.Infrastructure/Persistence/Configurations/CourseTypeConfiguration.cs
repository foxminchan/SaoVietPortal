using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class CourseTypeConfiguration : IEntityTypeConfiguration<CourseType>
{
    public void Configure(EntityTypeBuilder<CourseType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new CourseTypeId(v))
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Ignore(e => e.DomainEvents);
    }
}