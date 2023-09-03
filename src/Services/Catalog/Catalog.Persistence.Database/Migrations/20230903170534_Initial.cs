using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "descripcion 1", "nombre 1", 174m },
                    { 2, "descripcion 2", "nombre 2", 186m },
                    { 3, "descripcion 3", "nombre 3", 44m },
                    { 4, "descripcion 4", "nombre 4", 148m },
                    { 5, "descripcion 5", "nombre 5", 235m },
                    { 6, "descripcion 6", "nombre 6", 101m },
                    { 7, "descripcion 7", "nombre 7", 49m },
                    { 8, "descripcion 8", "nombre 8", 41m },
                    { 9, "descripcion 9", "nombre 9", 127m },
                    { 10, "descripcion 10", "nombre 10", 285m },
                    { 11, "descripcion 11", "nombre 11", 200m },
                    { 12, "descripcion 12", "nombre 12", 41m },
                    { 13, "descripcion 13", "nombre 13", 194m },
                    { 14, "descripcion 14", "nombre 14", 118m },
                    { 15, "descripcion 15", "nombre 15", 218m },
                    { 16, "descripcion 16", "nombre 16", 171m },
                    { 17, "descripcion 17", "nombre 17", 1m },
                    { 18, "descripcion 18", "nombre 18", 97m },
                    { 19, "descripcion 19", "nombre 19", 210m },
                    { 20, "descripcion 20", "nombre 20", 175m },
                    { 21, "descripcion 21", "nombre 21", 269m },
                    { 22, "descripcion 22", "nombre 22", 299m },
                    { 23, "descripcion 23", "nombre 23", 200m },
                    { 24, "descripcion 24", "nombre 24", 56m },
                    { 25, "descripcion 25", "nombre 25", 269m },
                    { 26, "descripcion 26", "nombre 26", 118m },
                    { 27, "descripcion 27", "nombre 27", 164m },
                    { 28, "descripcion 28", "nombre 28", 21m },
                    { 29, "descripcion 29", "nombre 29", 33m },
                    { 30, "descripcion 30", "nombre 30", 50m },
                    { 31, "descripcion 31", "nombre 31", 102m },
                    { 32, "descripcion 32", "nombre 32", 126m },
                    { 33, "descripcion 33", "nombre 33", 2m },
                    { 34, "descripcion 34", "nombre 34", 223m },
                    { 35, "descripcion 35", "nombre 35", 104m },
                    { 36, "descripcion 36", "nombre 36", 191m },
                    { 37, "descripcion 37", "nombre 37", 119m },
                    { 38, "descripcion 38", "nombre 38", 16m },
                    { 39, "descripcion 39", "nombre 39", 16m },
                    { 40, "descripcion 40", "nombre 40", 183m },
                    { 41, "descripcion 41", "nombre 41", 29m },
                    { 42, "descripcion 42", "nombre 42", 96m },
                    { 43, "descripcion 43", "nombre 43", 171m },
                    { 44, "descripcion 44", "nombre 44", 173m },
                    { 45, "descripcion 45", "nombre 45", 287m },
                    { 46, "descripcion 46", "nombre 46", 64m },
                    { 47, "descripcion 47", "nombre 47", 94m },
                    { 48, "descripcion 48", "nombre 48", 278m },
                    { 49, "descripcion 49", "nombre 49", 17m },
                    { 50, "descripcion 50", "nombre 50", 155m },
                    { 51, "descripcion 51", "nombre 51", 283m },
                    { 52, "descripcion 52", "nombre 52", 90m },
                    { 53, "descripcion 53", "nombre 53", 133m },
                    { 54, "descripcion 54", "nombre 54", 298m },
                    { 55, "descripcion 55", "nombre 55", 120m },
                    { 56, "descripcion 56", "nombre 56", 268m },
                    { 57, "descripcion 57", "nombre 57", 88m },
                    { 58, "descripcion 58", "nombre 58", 140m },
                    { 59, "descripcion 59", "nombre 59", 254m },
                    { 60, "descripcion 60", "nombre 60", 45m },
                    { 61, "descripcion 61", "nombre 61", 18m },
                    { 62, "descripcion 62", "nombre 62", 59m },
                    { 63, "descripcion 63", "nombre 63", 257m },
                    { 64, "descripcion 64", "nombre 64", 18m },
                    { 65, "descripcion 65", "nombre 65", 43m },
                    { 66, "descripcion 66", "nombre 66", 49m },
                    { 67, "descripcion 67", "nombre 67", 60m },
                    { 68, "descripcion 68", "nombre 68", 201m },
                    { 69, "descripcion 69", "nombre 69", 296m },
                    { 70, "descripcion 70", "nombre 70", 280m },
                    { 71, "descripcion 71", "nombre 71", 42m },
                    { 72, "descripcion 72", "nombre 72", 213m },
                    { 73, "descripcion 73", "nombre 73", 66m },
                    { 74, "descripcion 74", "nombre 74", 275m },
                    { 75, "descripcion 75", "nombre 75", 226m },
                    { 76, "descripcion 76", "nombre 76", 96m },
                    { 77, "descripcion 77", "nombre 77", 275m },
                    { 78, "descripcion 78", "nombre 78", 110m },
                    { 79, "descripcion 79", "nombre 79", 66m },
                    { 80, "descripcion 80", "nombre 80", 118m },
                    { 81, "descripcion 81", "nombre 81", 206m },
                    { 82, "descripcion 82", "nombre 82", 116m },
                    { 83, "descripcion 83", "nombre 83", 236m },
                    { 84, "descripcion 84", "nombre 84", 126m },
                    { 85, "descripcion 85", "nombre 85", 166m },
                    { 86, "descripcion 86", "nombre 86", 80m },
                    { 87, "descripcion 87", "nombre 87", 158m },
                    { 88, "descripcion 88", "nombre 88", 146m },
                    { 89, "descripcion 89", "nombre 89", 227m },
                    { 90, "descripcion 90", "nombre 90", 208m },
                    { 91, "descripcion 91", "nombre 91", 112m },
                    { 92, "descripcion 92", "nombre 92", 121m },
                    { 93, "descripcion 93", "nombre 93", 203m },
                    { 94, "descripcion 94", "nombre 94", 215m },
                    { 95, "descripcion 95", "nombre 95", 239m },
                    { 96, "descripcion 96", "nombre 96", 116m },
                    { 97, "descripcion 97", "nombre 97", 106m },
                    { 98, "descripcion 98", "nombre 98", 228m },
                    { 99, "descripcion 99", "nombre 99", 134m },
                    { 100, "descripcion 100", "nombre 100", 169m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 45 },
                    { 2, 2, 476 },
                    { 3, 3, 401 },
                    { 4, 4, 305 },
                    { 5, 5, 35 },
                    { 6, 6, 9 },
                    { 7, 7, 215 },
                    { 8, 8, 471 },
                    { 9, 9, 113 },
                    { 10, 10, 159 },
                    { 11, 11, 50 },
                    { 12, 12, 93 },
                    { 13, 13, 26 },
                    { 14, 14, 205 },
                    { 15, 15, 220 },
                    { 16, 16, 114 },
                    { 17, 17, 308 },
                    { 18, 18, 142 },
                    { 19, 19, 75 },
                    { 20, 20, 22 },
                    { 21, 21, 151 },
                    { 22, 22, 100 },
                    { 23, 23, 349 },
                    { 24, 24, 73 },
                    { 25, 25, 460 },
                    { 26, 26, 310 },
                    { 27, 27, 245 },
                    { 28, 28, 145 },
                    { 29, 29, 452 },
                    { 30, 30, 187 },
                    { 31, 31, 390 },
                    { 32, 32, 379 },
                    { 33, 33, 384 },
                    { 34, 34, 69 },
                    { 35, 35, 55 },
                    { 36, 36, 144 },
                    { 37, 37, 347 },
                    { 38, 38, 196 },
                    { 39, 39, 71 },
                    { 40, 40, 400 },
                    { 41, 41, 351 },
                    { 42, 42, 352 },
                    { 43, 43, 460 },
                    { 44, 44, 454 },
                    { 45, 45, 297 },
                    { 46, 46, 193 },
                    { 47, 47, 121 },
                    { 48, 48, 162 },
                    { 49, 49, 243 },
                    { 50, 50, 93 },
                    { 51, 51, 230 },
                    { 52, 52, 21 },
                    { 53, 53, 142 },
                    { 54, 54, 473 },
                    { 55, 55, 273 },
                    { 56, 56, 494 },
                    { 57, 57, 111 },
                    { 58, 58, 353 },
                    { 59, 59, 497 },
                    { 60, 60, 225 },
                    { 61, 61, 42 },
                    { 62, 62, 489 },
                    { 63, 63, 498 },
                    { 64, 64, 391 },
                    { 65, 65, 147 },
                    { 66, 66, 129 },
                    { 67, 67, 495 },
                    { 68, 68, 91 },
                    { 69, 69, 72 },
                    { 70, 70, 243 },
                    { 71, 71, 414 },
                    { 72, 72, 358 },
                    { 73, 73, 231 },
                    { 74, 74, 131 },
                    { 75, 75, 278 },
                    { 76, 76, 123 },
                    { 77, 77, 198 },
                    { 78, 78, 132 },
                    { 79, 79, 116 },
                    { 80, 80, 66 },
                    { 81, 81, 273 },
                    { 82, 82, 266 },
                    { 83, 83, 474 },
                    { 84, 84, 234 },
                    { 85, 85, 469 },
                    { 86, 86, 193 },
                    { 87, 87, 85 },
                    { 88, 88, 222 },
                    { 89, 89, 430 },
                    { 90, 90, 342 },
                    { 91, 91, 125 },
                    { 92, 92, 120 },
                    { 93, 93, 278 },
                    { 94, 94, 181 },
                    { 95, 95, 465 },
                    { 96, 96, 13 },
                    { 97, 97, 48 },
                    { 98, 98, 68 },
                    { 99, 99, 155 },
                    { 100, 100, 146 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductInStockId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductInStockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
