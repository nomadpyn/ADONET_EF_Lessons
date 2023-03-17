using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace l8_GameMigr.Migrations
{
    /// <inheritdoc />
    public partial class AddedYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleasedId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Year",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ReleasedId",
                table: "Games",
                column: "ReleasedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Year_ReleasedId",
                table: "Games",
                column: "ReleasedId",
                principalTable: "Year",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Year_ReleasedId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Year");

            migrationBuilder.DropIndex(
                name: "IX_Games_ReleasedId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleasedId",
                table: "Games");
        }
    }
}
