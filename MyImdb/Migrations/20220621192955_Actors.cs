using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyImdb.Migrations
{
    public partial class Actors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_GenreId",
                table: "Movies",
                newName: "IX_Movies_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthplace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieActors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_ActorId",
                table: "MovieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_MovieId",
                table: "MovieActors",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "MovieActors");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId",
                table: "Movie",
                newName: "IX_Movie_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
