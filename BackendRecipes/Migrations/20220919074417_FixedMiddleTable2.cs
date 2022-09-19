using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendRecipes.Migrations
{
    public partial class FixedMiddleTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "IngredientRecipes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IngredientRecipes",
                newName: "id");
        }
    }
}
