using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81137153-f5bd-4160-a5cc-743a239db3b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8964292-625c-4c4c-8af2-f900340d01bd");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b57c1be-670f-4d37-b2cf-642f7774d66f", null, "User", "USER" },
                    { "ea38e369-d22b-4ab5-a118-6da5bfd081cb", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b57c1be-670f-4d37-b2cf-642f7774d66f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea38e369-d22b-4ab5-a118-6da5bfd081cb");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81137153-f5bd-4160-a5cc-743a239db3b6", null, "Administrator", "ADMINISTRATOR" },
                    { "a8964292-625c-4c4c-8af2-f900340d01bd", null, "User", "USER" }
                });
        }
    }
}
