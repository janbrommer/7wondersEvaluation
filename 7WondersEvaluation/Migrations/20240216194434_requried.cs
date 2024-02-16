using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _7WondersEvaluation.Migrations
{
    /// <inheritdoc />
    public partial class requried : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_PlayersInGame_GameId_PlayerId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOutlay_PlayersInGame_GameId_PlayerId",
                table: "PlayerOutlay");

            migrationBuilder.AlterColumn<string>(
                name: "Gilds",
                table: "PlayerOutlay",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_PlayersInGame_GameId_PlayerId",
                table: "Evaluation",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "PlayersInGame",
                principalColumns: new[] { "GameId", "PlayerId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOutlay_PlayersInGame_GameId_PlayerId",
                table: "PlayerOutlay",
                columns: new[] { "GameId", "PlayerId" },
                principalTable: "PlayersInGame",
                principalColumns: new[] { "GameId", "PlayerId" },
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<string>(
                name: "Gilds",
                table: "PlayerOutlay",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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
    }
}
