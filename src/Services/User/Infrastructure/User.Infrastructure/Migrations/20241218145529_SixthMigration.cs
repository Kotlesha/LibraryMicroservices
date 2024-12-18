using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "User",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_User_ApplicationUserId",
                table: "User",
                newName: "IX_User_AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "User",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AccountId",
                table: "User",
                newName: "IX_User_ApplicationUserId");
        }
    }
}
