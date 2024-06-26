using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookStore.Migrations
{
    public partial class seedProductToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProdID",
                keyValue: 1,
                column: "ImageURL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProdID",
                keyValue: 2,
                column: "ImageURL",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProdID",
                keyValue: 1,
                column: "ImageURL",
                value: "123");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProdID",
                keyValue: 2,
                column: "ImageURL",
                value: "123");
        }
    }
}
