using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "wishListItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductvariantId",
                table: "wishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_ProductvariantId",
                table: "wishListItems",
                column: "ProductvariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_productVariants_ProductvariantId",
                table: "wishListItems",
                column: "ProductvariantId",
                principalTable: "productVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_productVariants_ProductvariantId",
                table: "wishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems");

            migrationBuilder.DropIndex(
                name: "IX_wishListItems_ProductvariantId",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "ProductvariantId",
                table: "wishListItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "wishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
