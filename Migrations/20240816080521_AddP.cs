using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_id", "created_at", "description", "inventory", "name", "price" },
                values: new object[] { new Guid("1caec261-2634-4b15-8371-e19f9946efa3"), new Guid("dcee6254-9093-401a-8394-77d2dbc9f922"), new DateTime(2024, 8, 16, 8, 5, 21, 150, DateTimeKind.Utc).AddTicks(2680), "test", 2, "p2", 12.199999999999999 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("1caec261-2634-4b15-8371-e19f9946efa3"));
        }
    }
}
