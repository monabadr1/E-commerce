using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_discounts_products_ProductId",
                table: "discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_products_ProductId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems");

            migrationBuilder.DropIndex(
                name: "IX_wishLists_UserId",
                table: "wishLists");

            migrationBuilder.DropIndex(
                name: "IX_wishListItems_ProductId",
                table: "wishListItems");

            migrationBuilder.DropIndex(
                name: "IX_productVariants_ProductId_size_color",
                table: "productVariants");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_discounts_ProductId",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "Imageurl",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "orderItems");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "productVariants",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "productVariants",
                newName: "Color");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_wishLists_UserId",
                table: "wishLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productVariants_ProductId_Size_Color",
                table: "productVariants",
                columns: new[] { "ProductId", "Size", "Color" },
                unique: true,
                filter: "[Size] IS NOT NULL AND [Color] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_products_DiscountId",
                table: "products",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "DiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_wishLists_UserId",
                table: "wishLists");

            migrationBuilder.DropIndex(
                name: "IX_productVariants_ProductId_Size_Color",
                table: "productVariants");

            migrationBuilder.DropIndex(
                name: "IX_products_DiscountId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "productVariants",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "productVariants",
                newName: "color");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "wishListItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imageurl",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "orderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_wishLists_UserId",
                table: "wishLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_ProductId",
                table: "wishListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productVariants_ProductId_size_color",
                table: "productVariants",
                columns: new[] { "ProductId", "size", "color" },
                unique: true,
                filter: "[size] IS NOT NULL AND [color] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_discounts_ProductId",
                table: "discounts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_discounts_products_ProductId",
                table: "discounts",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_products_ProductId",
                table: "orderItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_products_ProductId",
                table: "wishListItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");
        }
    }
}
