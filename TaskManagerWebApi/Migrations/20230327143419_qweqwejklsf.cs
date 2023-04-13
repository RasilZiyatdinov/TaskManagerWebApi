using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class qweqwejklsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "AspNetIdentity",
                table: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AspNetIdentity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "AspNetIdentity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "AspNetIdentity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "AspNetIdentity",
                table: "Role",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "AspNetIdentity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "1", "IdentityRole<int>", "Преподаватель", "ПРЕПОДАВАТЕЛЬ" },
                    { 2, "2", "IdentityRole<int>", "Студент", "СТУДЕНТ" },
                    { 3, "3", "IdentityRole<int>", "Менеджер", "МЕНЕДЖЕР" }
                });
        }
    }
}
