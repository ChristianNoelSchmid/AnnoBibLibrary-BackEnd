using Microsoft.EntityFrameworkCore.Migrations;

namespace AnnoBibLibrary.Migrations
{
    public partial class UserLibraryFunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Libraries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_AspNetUsers_UserId",
                table: "Libraries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_AspNetUsers_UserId",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Libraries");
        }
    }
}
