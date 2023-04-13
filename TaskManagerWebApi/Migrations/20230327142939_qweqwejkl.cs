using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class qweqwejkl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProject_Role_RequestRoleId",
                table: "StudentProject");

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

            migrationBuilder.RenameColumn(
                name: "RequestRoleId",
                table: "StudentProject",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProject_RequestRoleId",
                table: "StudentProject",
                newName: "IX_StudentProject_RoleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProject_Role_RoleId",
                table: "StudentProject",
                column: "RoleId",
                principalSchema: "AspNetIdentity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProject_Role_RoleId",
                table: "StudentProject");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "AspNetIdentity",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "StudentProject",
                newName: "RequestRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProject_RoleId",
                table: "StudentProject",
                newName: "IX_StudentProject_RequestRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProject_Role_RequestRoleId",
                table: "StudentProject",
                column: "RequestRoleId",
                principalSchema: "AspNetIdentity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
