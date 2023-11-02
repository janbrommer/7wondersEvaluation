using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _7WondersEvaluation.Migrations
{
    /// <inheritdoc />
    public partial class addPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGame_Games_GameId",
                table: "PlayerGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGame_Players_PlayerId",
                table: "PlayerGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame");

            migrationBuilder.RenameTable(
                name: "PlayerGame",
                newName: "PlayerGames");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGame_GameId",
                table: "PlayerGames",
                newName: "IX_PlayerGames_GameId");

            migrationBuilder.AddColumn<int>(
                name: "PositionInGame",
                table: "PlayerGames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGames",
                table: "PlayerGames",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGames_Games_GameId",
                table: "PlayerGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGames_Players_PlayerId",
                table: "PlayerGames",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGames_Games_GameId",
                table: "PlayerGames");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGames_Players_PlayerId",
                table: "PlayerGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGames",
                table: "PlayerGames");

            migrationBuilder.DropColumn(
                name: "PositionInGame",
                table: "PlayerGames");

            migrationBuilder.RenameTable(
                name: "PlayerGames",
                newName: "PlayerGame");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGames_GameId",
                table: "PlayerGame",
                newName: "IX_PlayerGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGame",
                table: "PlayerGame",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Games_GameId",
                table: "PlayerGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Players_PlayerId",
                table: "PlayerGame",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
