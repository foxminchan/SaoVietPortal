using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.AvatarUrl)
            .HasColumnType("varchar(max)");

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