using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethod");

        builder.HasKey(e => e.paymentMethodId);
        builder.Property(e => e.paymentMethodId)
            .HasColumnName("Id")
            .HasColumnType("tinyint")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.paymentMethodName)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();
    }
}