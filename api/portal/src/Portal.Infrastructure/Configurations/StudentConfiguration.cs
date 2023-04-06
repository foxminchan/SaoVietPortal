using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(e => e.studentId);

        builder.Property(e => e.studentId)
            .HasColumnName("Id")
            .HasColumnType("char(10)");

        builder.Property(e => e.fullname)
            .HasColumnName("Fullname")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.gender)
                .HasColumnName("Gender")
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();

        builder.Property(e => e.address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.dob)
            .HasConversion<StringConverter>()
            .HasColumnName("Dob")
            .HasColumnType("date");

        builder.Property(e => e.pod)
            .HasColumnName("Pod")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.occupation)
            .HasColumnName("Occupation")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.socialNetwork)
            .HasConversion<JsonConverter>()
            .HasColumnName("SocialNetwork")
            .HasColumnType("nvarchar(max)");
    }
}