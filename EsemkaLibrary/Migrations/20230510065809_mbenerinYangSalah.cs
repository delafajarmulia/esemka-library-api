using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsemkaLibrary.Migrations
{
    /// <inheritdoc />
    public partial class mbenerinYangSalah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookInformations_BookInformationIsbnId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BookInformationIsbnId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "BookInformationIsbnId",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookInformationIsbnId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookInformationIsbnId",
                table: "Authors",
                column: "BookInformationIsbnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookInformations_BookInformationIsbnId",
                table: "Authors",
                column: "BookInformationIsbnId",
                principalTable: "BookInformations",
                principalColumn: "Id");
        }
    }
}
