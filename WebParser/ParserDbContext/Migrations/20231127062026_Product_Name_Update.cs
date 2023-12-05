using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Product_Name_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.AddColumn<uint>(
                name: "NameId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_Products_NameId",
                table: "Products",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsNames_NameId",
                table: "Products",
                column: "NameId",
                principalTable: "ProductsNames",
                principalColumn: "ProductsNamesId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsNames_NameId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_NameId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
