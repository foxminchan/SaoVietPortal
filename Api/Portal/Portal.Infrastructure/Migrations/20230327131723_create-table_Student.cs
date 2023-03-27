using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtable_Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(10)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Address = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Pod = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    SocialNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Students", x => x.Id); });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
