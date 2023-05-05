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

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .HasColumnType("char(10)");

        builder.Property(e => e.Fullname)
            .HasColumnName("Fullname")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Gender)
                .HasColumnName("Gender")
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();

        builder.Property(e => e.Address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.Dob)
            .HasConversion<StringConverter>()
            .HasColumnName("Dob")
            .HasColumnType("date");

        builder.Property(e => e.Pod)
            .HasColumnName("Pod")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.Occupation)
            .HasColumnName("Occupation")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.SocialNetwork)
            .HasConversion<JsonConverter>()
            .HasColumnName("SocialNetwork")
            .HasColumnType("nvarchar(max)");
    }
}