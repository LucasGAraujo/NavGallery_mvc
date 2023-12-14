using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavGallery.Migrations
{
    public partial class DateTimeIncluindo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInsert",
                table: "Carros",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInsert",
                table: "Carros");
        }
    }
}
