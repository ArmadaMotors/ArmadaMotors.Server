using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmadaMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductAssetMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAsset_Products_ProductId",
                table: "ProductAsset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAsset",
                table: "ProductAsset");

            migrationBuilder.RenameTable(
                name: "ProductAsset",
                newName: "ProductAssets");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAsset_ProductId",
                table: "ProductAssets",
                newName: "IX_ProductAssets_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAssets",
                table: "ProductAssets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAssets_Products_ProductId",
                table: "ProductAssets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAssets_Products_ProductId",
                table: "ProductAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAssets",
                table: "ProductAssets");

            migrationBuilder.RenameTable(
                name: "ProductAssets",
                newName: "ProductAsset");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAssets_ProductId",
                table: "ProductAsset",
                newName: "IX_ProductAsset_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAsset",
                table: "ProductAsset",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAsset_Products_ProductId",
                table: "ProductAsset",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
