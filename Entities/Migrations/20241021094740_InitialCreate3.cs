using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190f2bb7-3105-4acb-b9bd-ecfecc003bf5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efd8e49c-4b50-4726-9d95-05964385add1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81137153-f5bd-4160-a5cc-743a239db3b6", null, "Administrator", "ADMINISTRATOR" },
                    { "a8964292-625c-4c4c-8af2-f900340d01bd", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81137153-f5bd-4160-a5cc-743a239db3b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8964292-625c-4c4c-8af2-f900340d01bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "190f2bb7-3105-4acb-b9bd-ecfecc003bf5", null, "User", "USER" },
                    { "efd8e49c-4b50-4726-9d95-05964385add1", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
