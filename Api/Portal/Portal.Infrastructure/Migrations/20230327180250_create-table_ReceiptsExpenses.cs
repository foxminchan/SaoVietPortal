using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtable_ReceiptsExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptsExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "Uniqueidentifier", nullable: false),
                    Type = table.Column<bool>(type: "Bit", nullable: false, defaultValue: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchId = table.Column<string>(type: "char(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptsExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptsExpenses_Branch",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptsExpenses_branchId",
                table: "ReceiptsExpenses",
                column: "branchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptsExpenses");
        }
    }
}
