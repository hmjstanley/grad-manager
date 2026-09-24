using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad_manager.Migrations
{
    /// <inheritdoc />
    public partial class RenameStatusToStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Applications",
                newName: "Stage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "Applications",
                newName: "Status");
        }
    }
}
