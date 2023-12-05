using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Product_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");
        }
    }
}
