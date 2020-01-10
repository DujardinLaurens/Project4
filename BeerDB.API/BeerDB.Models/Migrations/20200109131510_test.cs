using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Identity_UserID",
                table: "Reactie",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactie_Identity_UserID",
                table: "Reactie",
                column: "Identity_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactie_AspNetUsers_Identity_UserID",
                table: "Reactie",
                column: "Identity_UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactie_AspNetUsers_Identity_UserID",
                table: "Reactie");

            migrationBuilder.DropIndex(
                name: "IX_Reactie_Identity_UserID",
                table: "Reactie");

            migrationBuilder.DropColumn(
                name: "Identity_UserID",
                table: "Reactie");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
