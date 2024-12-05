using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toverland_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceToAttraction4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttractionId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_AttractionId",
                table: "Maintenances",
                column: "AttractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Attractions_AttractionId",
                table: "Maintenances",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Attractions_AttractionId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_AttractionId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "AttractionId",
                table: "Maintenances");
        }
    }
}
