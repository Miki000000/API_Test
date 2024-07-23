using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiTest.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "234c862d-11ef-46b8-8022-f1e56b88bd14", null, "Admin", "ADMIN" },
                    { "f6a76d9d-21d9-4fab-a753-ff2d69066dae", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "234c862d-11ef-46b8-8022-f1e56b88bd14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6a76d9d-21d9-4fab-a753-ff2d69066dae");
        }
    }
}
