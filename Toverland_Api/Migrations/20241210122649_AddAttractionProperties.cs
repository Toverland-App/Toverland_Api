using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toverland_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAttractionProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attractions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsOperational",
                table: "Attractions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MinHeight",
                table: "Attractions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningDate",
                table: "Attractions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "IsOperational",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "MinHeight",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "OpeningDate",
                table: "Attractions");
        }
    }
}
