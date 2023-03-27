using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtable_Staff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Dsw = table.Column<DateTime>(type: "date", nullable: true),
                    positionId = table.Column<int>(type: "int", nullable: false),
                    branchId = table.Column<string>(type: "char(8)", nullable: true),
                    managerId = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Branch",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Staff_Manager",
                        column: x => x.managerId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staff_Position",
                        column: x => x.positionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_branchId",
                table: "Staff",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_managerId",
                table: "Staff",
                column: "managerId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_positionId",
                table: "Staff",
                column: "positionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
