using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad_manager.Migrations
{
    /// <inheritdoc />
    public partial class AddFinishTasksField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "ApplicationTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "ApplicationTasks");
        }
    }
}
