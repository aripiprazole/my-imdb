using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyImdb.Migrations {
	public partial class Initial : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Movies",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Rank = table.Column<int>(type: "int", nullable: false),
					Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					StoryLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Movies", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(name: "Movies");
		}
	}
}
