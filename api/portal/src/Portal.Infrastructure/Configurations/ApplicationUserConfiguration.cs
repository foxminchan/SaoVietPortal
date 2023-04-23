using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.ImageUrl)
            .HasColumnName("ImageUrl")
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.Staff)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.StaffId)
            .HasConstraintName("FK_Users_Staff")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.StudentId)
            .HasConstraintName("FK_Users_Students")
            .OnDelete(DeleteBehavior.SetNull);
    }
}