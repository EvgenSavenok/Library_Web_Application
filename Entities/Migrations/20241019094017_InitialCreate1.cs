using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "281fcb8e-67cb-4ed7-b51a-75560d734c8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d06f5dc-4114-40f9-a80b-93bbaf91fb29");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45eb434b-d2e5-437a-8ade-bd9271abeef6", null, "Administrator", "ADMINISTRATOR" },
                    { "509addb4-4bcc-4556-8ac1-99dfb826731d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45eb434b-d2e5-437a-8ade-bd9271abeef6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "509addb4-4bcc-4556-8ac1-99dfb826731d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "281fcb8e-67cb-4ed7-b51a-75560d734c8b", null, "Administrator", "ADMINISTRATOR" },
                    { "3d06f5dc-4114-40f9-a80b-93bbaf91fb29", null, "User", "USER" }
                });
        }
    }
}
