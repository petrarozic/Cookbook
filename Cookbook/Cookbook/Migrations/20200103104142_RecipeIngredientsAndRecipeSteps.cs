using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookbook.Migrations
{
    public partial class RecipeIngredientsAndRecipeSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Steps",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Steps",
                table: "Recipes");
        }
    }
}
