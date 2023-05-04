using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staff");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("varchar(20)");

        builder.Property(e => e.Fullname)
            .HasColumnName("Fullname")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Dob)
            .HasConversion<StringConverter>()
            .HasColumnName("Dob")
            .HasColumnType("date");

        builder.Property(e => e.Address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.Dsw)
            .HasConversion<StringConverter>()
            .HasColumnName("Dsw")
            .HasColumnType("date");

        builder.HasOne(e => e.Position)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.PositionId)
            .HasConstraintName("FK_Staff_Position")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_Staff_Branch")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Manager)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.ManagerId)
            .HasConstraintName("FK_Staff_Manager")
            .OnDelete(DeleteBehavior.NoAction);
    }
}