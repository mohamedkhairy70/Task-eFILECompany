using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task_eFILECompany.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<byte[]>(type: "varbinary(900)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageDetails_Img",
                table: "ImageDetails",
                column: "Img",
                unique: true,
                filter: "[Img] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageDetails");
        }
    }
}
