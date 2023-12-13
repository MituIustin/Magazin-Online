using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazin_Online.Data.Migrations
{
    public partial class FKSS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_OrderId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Products_ProductId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ProductId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OrderId",
                table: "Baskets");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BasketId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BasketId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_BasketId",
                table: "Baskets",
                column: "BasketId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Products_RequestId",
                table: "Requests",
                column: "RequestId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_BasketId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Products_RequestId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Baskets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProductId",
                table: "Requests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OrderId",
                table: "Baskets",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_OrderId",
                table: "Baskets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Products_ProductId",
                table: "Requests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
