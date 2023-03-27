using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class altertable_FKUsersStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "paymentMethodId",
                table: "CourseRegistration",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "staffId",
                table: "AspNetUsers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_staffId",
                table: "AspNetUsers",
                column: "staffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Staff",
                table: "AspNetUsers",
                column: "staffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Staff",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_staffId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "staffId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte>(
                name: "paymentMethodId",
                table: "CourseRegistration",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }
    }
}
