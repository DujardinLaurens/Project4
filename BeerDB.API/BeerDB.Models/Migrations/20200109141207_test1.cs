using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDB.Models.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto",
                table: "Reactie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "foto",
                table: "Reactie",
                nullable: true);
        }
    }
}
