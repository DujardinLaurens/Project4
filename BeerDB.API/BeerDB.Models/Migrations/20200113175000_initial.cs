using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactie_AspNetUsers_Identity_UserID",
                table: "Reactie");

            migrationBuilder.DropIndex(
                name: "IX_Reactie_Identity_UserID",
                table: "Reactie");

            migrationBuilder.RenameColumn(
                name: "Identity_UserID",
                table: "Reactie",
                newName: "gebruiker");

            migrationBuilder.AlterColumn<string>(
                name: "gebruiker",
                table: "Reactie",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gebruiker",
                table: "Reactie",
                newName: "Identity_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Identity_UserID",
                table: "Reactie",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
