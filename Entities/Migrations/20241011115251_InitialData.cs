using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "BookTitle", "Description", "Genre", "ISBN", "ReceiptTime", "ReturnTime" },
                values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Vlados", "IT_Solutions Ltd", "AAAAA", 0, "00000000", new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));
        }
    }
}
