using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtable_StudentProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Position",
                table: "Staff");

            migrationBuilder.CreateTable(
                name: "StudentProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "Uniqueidentifier", nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    LessonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonDate = table.Column<DateTime>(type: "date", nullable: true),
                    ProgressStatus = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    LessonRating = table.Column<double>(type: "float", nullable: false),
                    staffId = table.Column<string>(type: "varchar(20)", nullable: true),
                    studentId = table.Column<string>(type: "char(10)", nullable: true),
                    classId = table.Column<string>(type: "char(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProgress_CourseEnrollment",
                        columns: x => new { x.studentId, x.classId },
                        principalTable: "CourseEnrollment",
                        principalColumns: new[] { "studentId", "classId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProgress_Staff",
                        column: x => x.staffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgress_staffId",
                table: "StudentProgress",
                column: "staffId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgress_studentId_classId",
                table: "StudentProgress",
                columns: new[] { "studentId", "classId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Position",
                table: "Staff",
                column: "positionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Position",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "StudentProgress");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Position",
                table: "Staff",
                column: "positionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
