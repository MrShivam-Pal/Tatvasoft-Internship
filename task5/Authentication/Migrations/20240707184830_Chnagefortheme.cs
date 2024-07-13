using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class Chnagefortheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Themes_ThemeId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_ThemeId",
                table: "Missions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Missions_ThemeId",
                table: "Missions",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Themes_ThemeId",
                table: "Missions",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "ThemeId");
        }
    }
}
