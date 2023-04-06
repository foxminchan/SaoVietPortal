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

        builder.HasKey(e => e.staffId);

        builder.Property(e => e.staffId)
            .HasColumnName("Id")
            .HasColumnType("varchar(20)");

        builder.Property(e => e.fullname)
            .HasColumnName("Fullname")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.dob)
            .HasConversion<StringConverter>()
            .HasColumnName("Dob")
            .HasColumnType("date");

        builder.Property(e => e.address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.dsw)
            .HasConversion<StringConverter>()
            .HasColumnName("Dsw")
            .HasColumnType("date");

        builder.HasOne(e => e.position)
            .WithMany(e => e.staffs)
            .HasForeignKey(e => e.positionId)
            .HasConstraintName("FK_Staff_Position")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.branch)
            .WithMany(e => e.staffs)
            .HasForeignKey(e => e.branchId)
            .HasConstraintName("FK_Staff_Branch")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.manager)
            .WithMany(e => e.staffs)
            .HasForeignKey(e => e.managerId)
            .HasConstraintName("FK_Staff_Manager")
            .OnDelete(DeleteBehavior.NoAction);
    }
}