using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyImdb.Migrations
{
    public partial class AddCharacterToActor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "MovieActors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Character",
                table: "MovieActors");
        }
    }
}
