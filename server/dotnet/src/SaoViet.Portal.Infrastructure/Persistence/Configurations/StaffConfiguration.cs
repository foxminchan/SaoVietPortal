using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence.Comparers;
using SaoViet.Portal.Infrastructure.Persistence.Converters;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new StaffId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.UuidSequential);

        builder.Property(e => e.Fullname)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.DateOfBirth)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.OwnsOne(e => e.Address, sb => sb.ToJson());

        builder.Property(e => e.DateStartWork)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder.HasOne(e => e.Position)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.PositionId)
            .HasConstraintName("FK_Staff_Position")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_Staff_Branch")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Manager)
            .WithMany(e => e.Staffs)
            .HasForeignKey(e => e.ManagerId)
            .HasConstraintName("FK_Staff_Manager")
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(e => e.DomainEvents);
    }
}