using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task_eFILECompany.Migrations
{
    public partial class RemovePropUniqueToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ImageDetails_Img",
                table: "ImageDetails");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Img",
                table: "ImageDetails",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(900)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Img",
                table: "ImageDetails",
                type: "varbinary(900)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageDetails_Img",
                table: "ImageDetails",
                column: "Img",
                unique: true,
                filter: "[Img] IS NOT NULL");
        }
    }
}
