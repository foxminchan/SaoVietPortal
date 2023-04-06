using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helpers;

namespace Portal.Infrastructure.Configurations;

public class ReceiptsExpensesConfiguration : IEntityTypeConfiguration<ReceiptsExpenses>
{
    public void Configure(EntityTypeBuilder<ReceiptsExpenses> builder)
    {
        builder.ToTable("ReceiptsExpenses");

        builder.HasKey(e => e.receiptExpenseId);
        builder.Property(e => e.receiptExpenseId)
            .HasColumnName("Id")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.type)
            .HasColumnName("Type")
            .HasColumnType("Bit")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.date)
            .HasConversion<StringConverter>()
            .HasColumnName("Date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.amount)
            .HasColumnName("Amount")
            .HasColumnType("float")
            .IsRequired();

        builder.Property(e => e.note)
            .HasColumnName("Note")
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.branch)
            .WithMany(e => e.receiptsExpenses)
            .HasForeignKey(e => e.branchId)
            .HasConstraintName("FK_ReceiptsExpenses_Branch")
            .OnDelete(DeleteBehavior.SetNull);
    }
}