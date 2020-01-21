using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class commentChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "beerId",
                table: "Reactie",
                newName: "selectedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "selectedId",
                table: "Reactie",
                newName: "beerId");
        }
    }
}
