﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Customer.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Customer",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "Customer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Cliente 1" },
                    { 2, "Cliente 2" },
                    { 3, "Cliente 3" },
                    { 4, "Cliente 4" },
                    { 5, "Cliente 5" },
                    { 6, "Cliente 6" },
                    { 7, "Cliente 7" },
                    { 8, "Cliente 8" },
                    { 9, "Cliente 9" },
                    { 10, "Cliente 10" },
                    { 11, "Cliente 11" },
                    { 12, "Cliente 12" },
                    { 13, "Cliente 13" },
                    { 14, "Cliente 14" },
                    { 15, "Cliente 15" },
                    { 16, "Cliente 16" },
                    { 17, "Cliente 17" },
                    { 18, "Cliente 18" },
                    { 19, "Cliente 19" },
                    { 20, "Cliente 20" },
                    { 21, "Cliente 21" },
                    { 22, "Cliente 22" },
                    { 23, "Cliente 23" },
                    { 24, "Cliente 24" },
                    { 25, "Cliente 25" },
                    { 26, "Cliente 26" },
                    { 27, "Cliente 27" },
                    { 28, "Cliente 28" },
                    { 29, "Cliente 29" },
                    { 30, "Cliente 30" },
                    { 31, "Cliente 31" },
                    { 32, "Cliente 32" },
                    { 33, "Cliente 33" },
                    { 34, "Cliente 34" },
                    { 35, "Cliente 35" },
                    { 36, "Cliente 36" },
                    { 37, "Cliente 37" },
                    { 38, "Cliente 38" },
                    { 39, "Cliente 39" },
                    { 40, "Cliente 40" },
                    { 41, "Cliente 41" },
                    { 42, "Cliente 42" },
                    { 43, "Cliente 43" },
                    { 44, "Cliente 44" },
                    { 45, "Cliente 45" },
                    { 46, "Cliente 46" },
                    { 47, "Cliente 47" },
                    { 48, "Cliente 48" },
                    { 49, "Cliente 49" },
                    { 50, "Cliente 50" },
                    { 51, "Cliente 51" },
                    { 52, "Cliente 52" },
                    { 53, "Cliente 53" },
                    { 54, "Cliente 54" },
                    { 55, "Cliente 55" },
                    { 56, "Cliente 56" },
                    { 57, "Cliente 57" },
                    { 58, "Cliente 58" },
                    { 59, "Cliente 59" },
                    { 60, "Cliente 60" },
                    { 61, "Cliente 61" },
                    { 62, "Cliente 62" },
                    { 63, "Cliente 63" },
                    { 64, "Cliente 64" },
                    { 65, "Cliente 65" },
                    { 66, "Cliente 66" },
                    { 67, "Cliente 67" },
                    { 68, "Cliente 68" },
                    { 69, "Cliente 69" },
                    { 70, "Cliente 70" },
                    { 71, "Cliente 71" },
                    { 72, "Cliente 72" },
                    { 73, "Cliente 73" },
                    { 74, "Cliente 74" },
                    { 75, "Cliente 75" },
                    { 76, "Cliente 76" },
                    { 77, "Cliente 77" },
                    { 78, "Cliente 78" },
                    { 79, "Cliente 79" },
                    { 80, "Cliente 80" },
                    { 81, "Cliente 81" },
                    { 82, "Cliente 82" },
                    { 83, "Cliente 83" },
                    { 84, "Cliente 84" },
                    { 85, "Cliente 85" },
                    { 86, "Cliente 86" },
                    { 87, "Cliente 87" },
                    { 88, "Cliente 88" },
                    { 89, "Cliente 89" },
                    { 90, "Cliente 90" },
                    { 91, "Cliente 91" },
                    { 92, "Cliente 92" },
                    { 93, "Cliente 93" },
                    { 94, "Cliente 94" },
                    { 95, "Cliente 95" },
                    { 96, "Cliente 96" },
                    { 97, "Cliente 97" },
                    { 98, "Cliente 98" },
                    { 99, "Cliente 99" },
                    { 100, "Cliente 100" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                schema: "Customer",
                table: "Clients",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Customer");
        }
    }
}
