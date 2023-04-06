using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.imageUrl)
            .HasColumnName("ImageUrl")
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.staff)
            .WithMany(e => e.users)
            .HasForeignKey(e => e.staffId)
            .HasConstraintName("FK_Users_Staff")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.student)
            .WithMany(e => e.users)
            .HasForeignKey(e => e.studentId)
            .HasConstraintName("FK_Users_Students")
            .OnDelete(DeleteBehavior.SetNull);
    }
}