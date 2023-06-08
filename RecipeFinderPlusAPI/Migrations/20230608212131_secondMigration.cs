using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeFinderPlusAPI.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Recipes",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Recipes",
                newName: "Name");
        }
    }
}
