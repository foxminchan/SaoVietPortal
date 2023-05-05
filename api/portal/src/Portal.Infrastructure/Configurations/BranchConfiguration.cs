using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branch");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .HasColumnType("char(8)");

        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.Address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(80)");

        builder.Property(e => e.Phone)
            .HasColumnName("Phone")
            .HasColumnType("char(10)");
    }
}