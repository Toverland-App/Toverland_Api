using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toverland_Api.Migrations
{
    /// <inheritdoc />
    public partial class Maintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Maintenances",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Maintenances",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Maintenances");
        }
    }
}
