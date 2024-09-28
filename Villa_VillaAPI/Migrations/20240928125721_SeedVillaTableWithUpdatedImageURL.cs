using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Villa_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTableWithUpdatedImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 27, 21, 505, DateTimeKind.Local).AddTicks(610), "https://raw.githubusercontent.com/gopalakrishnan94/Villa/refs/heads/main/Images/villa_01.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 27, 21, 505, DateTimeKind.Local).AddTicks(630), "https://raw.githubusercontent.com/gopalakrishnan94/Villa/refs/heads/main/Images/villa_02.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 27, 21, 505, DateTimeKind.Local).AddTicks(640), "https://raw.githubusercontent.com/gopalakrishnan94/Villa/refs/heads/main/Images/villa_03.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 27, 21, 505, DateTimeKind.Local).AddTicks(640), "https://raw.githubusercontent.com/gopalakrishnan94/Villa/refs/heads/main/Images/villa_04.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 27, 21, 505, DateTimeKind.Local).AddTicks(640), "https://raw.githubusercontent.com/gopalakrishnan94/Villa/refs/heads/main/Images/villa_05.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 18, 13, 922, DateTimeKind.Local).AddTicks(9240), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 18, 13, 922, DateTimeKind.Local).AddTicks(9320), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 18, 13, 922, DateTimeKind.Local).AddTicks(9320), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 18, 13, 922, DateTimeKind.Local).AddTicks(9320), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 28, 18, 18, 13, 922, DateTimeKind.Local).AddTicks(9330), "" });
        }
    }
}
