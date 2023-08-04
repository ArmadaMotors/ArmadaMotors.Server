using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmadaMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyType",
                table: "matorEngines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyType",
                table: "matorEngines");
        }
    }
}
