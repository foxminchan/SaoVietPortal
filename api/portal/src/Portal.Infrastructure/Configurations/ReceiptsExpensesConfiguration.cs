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

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("StudentId")
            .HasColumnType("Uniqueidentifier")
            .HasDefaultValueSql("newid()");

        builder.Property(e => e.Type)
            .HasColumnName("Type")
            .HasColumnType("Bit")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.Date)
            .HasConversion<StringConverter>()
            .HasColumnName("Date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.Amount)
            .HasColumnName("Amount")
            .HasColumnType("float")
            .IsRequired();

        builder.Property(e => e.Note)
            .HasColumnName("Note")
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.ReceiptsExpenses)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_ReceiptsExpenses_Branch")
            .OnDelete(DeleteBehavior.SetNull);
    }
}