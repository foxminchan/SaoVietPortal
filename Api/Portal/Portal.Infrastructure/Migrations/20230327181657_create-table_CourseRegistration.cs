using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtable_CourseRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "Uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "date", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegisterFee = table.Column<double>(type: "float", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    paymentMethodId = table.Column<byte>(type: "tinyint", nullable: true),
                    studentId = table.Column<string>(type: "char(10)", nullable: true),
                    classId = table.Column<string>(type: "char(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_PaymentMethod",
                        column: x => x.paymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_CourseEnrollment",
                        columns: x => new { x.studentId, x.classId },
                        principalTable: "CourseEnrollment",
                        principalColumns: new[] { "studentId", "classId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_paymentMethodId",
                table: "CourseRegistration",
                column: "paymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_studentId_classId",
                table: "CourseRegistration",
                columns: new[] { "studentId", "classId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseRegistration");
        }
    }
}
