using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobSphere.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSalaryPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3404));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3744));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3745));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3746));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3746));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3748));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3748));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3749));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 35, 25, 543, DateTimeKind.Utc).AddTicks(3750));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(1998));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2364));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 3, 11, 32, 1, 527, DateTimeKind.Utc).AddTicks(2366));
        }
    }
}
