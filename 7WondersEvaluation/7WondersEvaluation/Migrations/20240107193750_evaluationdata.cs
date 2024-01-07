using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _7WondersEvaluation.Migrations
{
    /// <inheritdoc />
    public partial class evaluationdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationGameId",
                table: "PlayersInGame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationPlayerId",
                table: "PlayersInGame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerOutlayGameId",
                table: "PlayersInGame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerOutlayPlayerId",
                table: "PlayersInGame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Red = table.Column<int>(type: "INTEGER", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpansionStages = table.Column<int>(type: "INTEGER", nullable: false),
                    Blue = table.Column<int>(type: "INTEGER", nullable: false),
                    Yellow = table.Column<int>(type: "INTEGER", nullable: false),
                    Violet = table.Column<int>(type: "INTEGER", nullable: false),
                    Green = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => new { x.GameId, x.PlayerId });
                });

            migrationBuilder.CreateTable(
                name: "PlayerOutlay",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountRed = table.Column<int>(type: "INTEGER", nullable: false),
                    CountGreen = table.Column<int>(type: "INTEGER", nullable: false),
                    CountYellow = table.Column<int>(type: "INTEGER", nullable: false),
                    CountBrown = table.Column<int>(type: "INTEGER", nullable: false),
                    CountGrey = table.Column<int>(type: "INTEGER", nullable: false),
                    CountGild = table.Column<int>(type: "INTEGER", nullable: false),
                    CountExpa = table.Column<int>(type: "INTEGER", nullable: false),
                    CountWarMarker = table.Column<int>(type: "INTEGER", nullable: false),
                    CountNegWarMarker = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOutlay", x => new { x.GameId, x.PlayerId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersInGame_EvaluationGameId_EvaluationPlayerId",
                table: "PlayersInGame",
                columns: new[] { "EvaluationGameId", "EvaluationPlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersInGame_PlayerOutlayGameId_PlayerOutlayPlayerId",
                table: "PlayersInGame",
                columns: new[] { "PlayerOutlayGameId", "PlayerOutlayPlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersInGame_Evaluation_EvaluationGameId_EvaluationPlayerId",
                table: "PlayersInGame",
                columns: new[] { "EvaluationGameId", "EvaluationPlayerId" },
                principalTable: "Evaluation",
                principalColumns: new[] { "GameId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersInGame_PlayerOutlay_PlayerOutlayGameId_PlayerOutlayPlayerId",
                table: "PlayersInGame",
                columns: new[] { "PlayerOutlayGameId", "PlayerOutlayPlayerId" },
                principalTable: "PlayerOutlay",
                principalColumns: new[] { "GameId", "PlayerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersInGame_Evaluation_EvaluationGameId_EvaluationPlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersInGame_PlayerOutlay_PlayerOutlayGameId_PlayerOutlayPlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "PlayerOutlay");

            migrationBuilder.DropIndex(
                name: "IX_PlayersInGame_EvaluationGameId_EvaluationPlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropIndex(
                name: "IX_PlayersInGame_PlayerOutlayGameId_PlayerOutlayPlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropColumn(
                name: "EvaluationGameId",
                table: "PlayersInGame");

            migrationBuilder.DropColumn(
                name: "EvaluationPlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropColumn(
                name: "PlayerOutlayGameId",
                table: "PlayersInGame");

            migrationBuilder.DropColumn(
                name: "PlayerOutlayPlayerId",
                table: "PlayersInGame");
        }
    }
}
