using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aaf5411-a5a0-4365-9849-2e990fabe67d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2883d28a-f7bc-47e1-80d8-09091f94cda1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51dbc97b-d87d-46fb-9225-cfc3fcacabc2", null, "Admin", "ADMIN" },
                    { "98c2b39a-9a99-44a0-9c55-6dd98e48e919", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51dbc97b-d87d-46fb-9225-cfc3fcacabc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98c2b39a-9a99-44a0-9c55-6dd98e48e919");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aaf5411-a5a0-4365-9849-2e990fabe67d", null, "User", "USER" },
                    { "2883d28a-f7bc-47e1-80d8-09091f94cda1", null, "Admin", "ADMIN" }
                });
        }
    }
}
