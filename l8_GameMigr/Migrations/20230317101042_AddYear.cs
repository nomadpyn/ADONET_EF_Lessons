using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace l8_GameMigr.Migrations
{
    /// <inheritdoc />
    public partial class AddYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_YearId",
                table: "Games",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Years_YearId",
                table: "Games",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Years_YearId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropIndex(
                name: "IX_Games_YearId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Games");
        }
    }
}
