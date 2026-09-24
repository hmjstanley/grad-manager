using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad_manager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Applications");
        }
    }
}
