using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFieldIdInPlacement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Placements",
                newName: "PlacementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlacementId",
                table: "Placements",
                newName: "Id");
        }
    }
}
