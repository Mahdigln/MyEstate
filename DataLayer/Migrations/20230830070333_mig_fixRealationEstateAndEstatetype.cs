using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class mig_fixRealationEstateAndEstatetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateTypes_Estates_EstateId",
                table: "EstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_EstateTypes_EstateId",
                table: "EstateTypes");

            migrationBuilder.DropColumn(
                name: "EstateId",
                table: "EstateTypes");

            migrationBuilder.AddColumn<int>(
                name: "EstateTypeId",
                table: "Estates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estates_EstateTypeId",
                table: "Estates",
                column: "EstateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_EstateTypes_EstateTypeId",
                table: "Estates",
                column: "EstateTypeId",
                principalTable: "EstateTypes",
                principalColumn: "EstateTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_EstateTypes_EstateTypeId",
                table: "Estates");

            migrationBuilder.DropIndex(
                name: "IX_Estates_EstateTypeId",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "EstateTypeId",
                table: "Estates");

            migrationBuilder.AddColumn<int>(
                name: "EstateId",
                table: "EstateTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypes_EstateId",
                table: "EstateTypes",
                column: "EstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateTypes_Estates_EstateId",
                table: "EstateTypes",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "EstateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
