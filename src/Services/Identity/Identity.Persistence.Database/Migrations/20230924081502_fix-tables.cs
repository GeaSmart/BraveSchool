using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class fixtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53279ecb-09c8-4e2f-b02d-58ddbebd44e8");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f690b84f-1f03-49a4-b179-60afa6f8fbe6", "2741b12d-3916-4905-8439-e5336abb9bff", "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f690b84f-1f03-49a4-b179-60afa6f8fbe6");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53279ecb-09c8-4e2f-b02d-58ddbebd44e8", "425128ed-6813-43fa-bd35-5072218d0b1d", "Admin", "ADMIN" });
        }
    }
}
