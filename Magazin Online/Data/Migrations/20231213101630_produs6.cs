using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazin_Online.Data.Migrations
{
    public partial class produs6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserIdId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserIdId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserIdId",
                table: "Products",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
