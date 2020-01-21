using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactie",
                table: "Reactie");

            migrationBuilder.RenameTable(
                name: "Reactie",
                newName: "Comment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Reactie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactie",
                table: "Reactie",
                column: "Id");
        }
    }
}
