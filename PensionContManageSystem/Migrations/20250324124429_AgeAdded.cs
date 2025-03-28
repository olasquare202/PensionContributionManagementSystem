using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PensionContManageSystem.Migrations
{
    /// <inheritdoc />
    public partial class AgeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "members");
        }
    }
}
