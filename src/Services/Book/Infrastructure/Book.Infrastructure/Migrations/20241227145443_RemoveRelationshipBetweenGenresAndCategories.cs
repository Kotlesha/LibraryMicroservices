using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationshipBetweenGenresAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGenre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryGenre",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGenre", x => new { x.CategoriesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_CategoryGenre_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGenre_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGenre_GenresId",
                table: "CategoryGenre",
                column: "GenresId");
        }
    }
}
