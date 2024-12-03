using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toverland_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Attractions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_AreaId",
                table: "Attractions",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_Areas_AreaId",
                table: "Attractions",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_Areas_AreaId",
                table: "Attractions");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_AreaId",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Attractions");
        }
    }
}
