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
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersInGame_Evaluation_GameId_PlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersInGame_PlayerOutlay_GameId_PlayerId",
                table: "PlayersInGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerOutlay",
                table: "PlayerOutlay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evaluation",
                table: "Evaluation");

            migrationBuilder.AddColumn<int>(
                name: "PlayerOutlayId",
                table: "PlayerOutlay",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationId",
                table: "Evaluation",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerOutlay",
                table: "PlayerOutlay",
                column: "PlayerOutlayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evaluation",
                table: "Evaluation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOutlay_GameId_PlayerId",
                table: "PlayerOutlay",
                columns: new[] { "GameId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_GameId_PlayerId",
                table: "Evaluation",
                columns: new[] { "GameId", "PlayerId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_PlayersInGame_GameId_PlayerId",
                table: "Evaluation",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "PlayersInGame",
                principalColumns: new[] { "GameId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOutlay_PlayersInGame_GameId_PlayerId",
                table: "PlayerOutlay",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "PlayersInGame",
                principalColumns: new[] { "GameId", "PlayerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_PlayersInGame_GameId_PlayerId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOutlay_PlayersInGame_GameId_PlayerId",
                table: "PlayerOutlay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerOutlay",
                table: "PlayerOutlay");

            migrationBuilder.DropIndex(
                name: "IX_PlayerOutlay_GameId_PlayerId",
                table: "PlayerOutlay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evaluation",
                table: "Evaluation");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_GameId_PlayerId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "PlayerOutlayId",
                table: "PlayerOutlay");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "Evaluation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerOutlay",
                table: "PlayerOutlay",
                columns: new[] { "GameId", "PlayerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evaluation",
                table: "Evaluation",
                columns: new[] { "GameId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersInGame_Evaluation_GameId_PlayerId",
                table: "PlayersInGame",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "Evaluation",
                principalColumns: new[] { "GameId", "PlayerId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersInGame_PlayerOutlay_GameId_PlayerId",
                table: "PlayersInGame",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "PlayerOutlay",
                principalColumns: new[] { "GameId", "PlayerId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
