using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branch");

        builder.HasKey(e => e.branchId);

        builder.Property(e => e.branchId)
            .HasColumnName("Id")
            .HasColumnType("char(8)");

        builder.Property(e => e.branchName)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.phone)
            .HasColumnName("Phone")
            .HasColumnType("char(10)");
    }
}