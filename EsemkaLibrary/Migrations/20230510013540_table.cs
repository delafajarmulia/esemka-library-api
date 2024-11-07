using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsemkaLibrary.Migrations
{
    /// <inheritdoc />
    public partial class table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTadon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShelfCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookInformationIsbnId = table.Column<int>(type: "int", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookInformations_BookInformationIsbnId",
                        column: x => x.BookInformationIsbnId,
                        principalTable: "BookInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorrowAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailUserId = table.Column<int>(type: "int", nullable: true),
                    CodeBookId = table.Column<int>(type: "int", nullable: true),
                    BookInformationIsbnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrows_BookInformations_BookInformationIsbnId",
                        column: x => x.BookInformationIsbnId,
                        principalTable: "BookInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Borrows_Books_CodeBookId",
                        column: x => x.CodeBookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Borrows_Users_EmailUserId",
                        column: x => x.EmailUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookId",
                table: "Books",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookInformationIsbnId",
                table: "Books",
                column: "BookInformationIsbnId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_BookInformationIsbnId",
                table: "Borrows",
                column: "BookInformationIsbnId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_CodeBookId",
                table: "Borrows",
                column: "CodeBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_EmailUserId",
                table: "Borrows",
                column: "EmailUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
