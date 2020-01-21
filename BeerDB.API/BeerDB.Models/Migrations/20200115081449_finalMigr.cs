using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class finalMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bierId",
                table: "Reactie");

            migrationBuilder.DropColumn(
                name: "commentaar",
                table: "Reactie");

            migrationBuilder.DropColumn(
                name: "gebruiker",
                table: "Reactie");

            migrationBuilder.AddColumn<string>(
                name: "beerId",
                table: "Reactie",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "Reactie",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "Reactie",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "beerId",
                table: "Reactie");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "Reactie");

            migrationBuilder.DropColumn(
                name: "user",
                table: "Reactie");

            migrationBuilder.AddColumn<string>(
                name: "bierId",
                table: "Reactie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "commentaar",
                table: "Reactie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gebruiker",
                table: "Reactie",
                nullable: true);
        }
    }
}
