using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ace7c3f-0c18-42d2-82a2-bb0d8ec881f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc30e264-f778-4333-8f37-ad8b3c441e41");

            migrationBuilder.DropColumn(
                name: "ReceiptTime",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReturnTime",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "BookTitle",
                table: "Books",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e11f201-46ae-4b1f-a96e-9d54a1fe0c22", null, "User", "USER" },
                    { "96871ff2-d150-4453-a8f3-9127006392ba", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Amount", "Genre" },
                values: new object[] { 0, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Amount", "Genre" },
                values: new object[] { 0, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e11f201-46ae-4b1f-a96e-9d54a1fe0c22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96871ff2-d150-4453-a8f3-9127006392ba");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "BookTitle",
                table: "Books",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiptTime",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnTime",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ace7c3f-0c18-42d2-82a2-bb0d8ec881f7", null, "User", "USER" },
                    { "fc30e264-f778-4333-8f37-ad8b3c441e41", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Genre", "ReceiptTime", "ReturnTime" },
                values: new object[] { 0, new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Genre", "ReceiptTime", "ReturnTime" },
                values: new object[] { 0, new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) });
        }
    }
}
