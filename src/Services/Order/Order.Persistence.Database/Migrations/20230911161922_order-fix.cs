using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class orderfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                schema: "Order",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "Order",
                newName: "Orders",
                newSchema: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderId",
                schema: "Order",
                table: "Orders",
                newName: "IX_Orders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "Order",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderId",
                principalSchema: "Order",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "Order",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "Order",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "Order",
                newName: "Order",
                newSchema: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderId",
                schema: "Order",
                table: "Order",
                newName: "IX_Order_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderId",
                principalSchema: "Order",
                principalTable: "Order",
                principalColumn: "OrderId");
        }
    }
}
