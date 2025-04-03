using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobSphere.Migrations
{
    /// <inheritdoc />
    public partial class seedingTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(1998), "Software Development", null },
                    { 2, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2360), "Marketing", null },
                    { 3, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2361), "Remote", null },
                    { 4, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2362), "Finance", null },
                    { 5, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2363), "Sales", null },
                    { 6, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2363), "Healthcare", null },
                    { 7, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2364), "Customer Support", null },
                    { 8, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2365), "Project Management", null },
                    { 9, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2365), "Engineering", null },
                    { 10, new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2366), "Education", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
