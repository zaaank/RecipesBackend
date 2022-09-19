using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendRecipes.Migrations
{
    public partial class changedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "storeName",
                table: "Ingredients",
                newName: "StoreName");

            migrationBuilder.RenameColumn(
                name: "mark",
                table: "Ingredients",
                newName: "Mark");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreName",
                table: "Ingredients",
                newName: "storeName");

            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "Ingredients",
                newName: "mark");
        }
    }
}
