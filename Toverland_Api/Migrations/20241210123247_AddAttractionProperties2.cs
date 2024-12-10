using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toverland_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAttractionProperties2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOperational",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "OpeningDate",
                table: "Attractions");

            migrationBuilder.AlterColumn<double>(
                name: "MinHeight",
                table: "Attractions",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Attractions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Attractions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingTime",
                table: "Attractions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningTime",
                table: "Attractions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QueueLength",
                table: "Attractions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QueueSpeed",
                table: "Attractions",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "OpeningTime",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "QueueLength",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "QueueSpeed",
                table: "Attractions");

            migrationBuilder.AlterColumn<double>(
                name: "MinHeight",
                table: "Attractions",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Attractions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperational",
                table: "Attractions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningDate",
                table: "Attractions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
