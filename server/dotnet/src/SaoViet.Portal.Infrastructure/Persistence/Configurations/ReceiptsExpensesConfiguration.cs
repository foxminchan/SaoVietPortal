using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence.Helpers;

namespace SaoViet.Portal.Infrastructure.Persistence.Configurations;

public class ReceiptsExpensesConfiguration : IEntityTypeConfiguration<ReceiptsExpenses>
{
    public void Configure(EntityTypeBuilder<ReceiptsExpenses> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(v => v.Value,
                v => new ReceiptsExpensesId(v))
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql(GuidHelper.Uuid);

        builder.Property(e => e.Type)
            .HasColumnType("Bit")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.ReceiptsExpenseDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(e => e.Amount)
            .HasColumnType("money")
            .IsRequired();

        builder.Property(e => e.Note)
            .HasColumnType("nvarchar(max)");

        builder.HasOne(e => e.Branch)
            .WithMany(e => e.ReceiptsExpenses)
            .HasForeignKey(e => e.BranchId)
            .HasConstraintName("FK_ReceiptsExpenses_Branch")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}