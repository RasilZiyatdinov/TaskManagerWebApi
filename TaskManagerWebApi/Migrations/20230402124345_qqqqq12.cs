using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class qqqqq12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask");

            migrationBuilder.DropIndex(
                name: "IX_StudentTask_StudentId",
                table: "StudentTask");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "StudentTask",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask",
                columns: new[] { "StudentId", "TaskId" });

            migrationBuilder.CreateTable(
                name: "TaskEntityUser",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "integer", nullable: false),
                    TaskEntitiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntityUser", x => new { x.StudentsId, x.TaskEntitiesId });
                    table.ForeignKey(
                        name: "FK_TaskEntityUser_Task_TaskEntitiesId",
                        column: x => x.TaskEntitiesId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEntityUser_User_StudentsId",
                        column: x => x.StudentsId,
                        principalSchema: "AspNetIdentity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_RoleId",
                table: "StudentTask",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_TaskId",
                table: "StudentTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntityUser_TaskEntitiesId",
                table: "TaskEntityUser",
                column: "TaskEntitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTask_TeamRole_RoleId",
                table: "StudentTask",
                column: "RoleId",
                principalTable: "TeamRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTask_TeamRole_RoleId",
                table: "StudentTask");

            migrationBuilder.DropTable(
                name: "TaskEntityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask");

            migrationBuilder.DropIndex(
                name: "IX_StudentTask_RoleId",
                table: "StudentTask");

            migrationBuilder.DropIndex(
                name: "IX_StudentTask_TaskId",
                table: "StudentTask");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "StudentTask");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTask",
                table: "StudentTask",
                columns: new[] { "TaskId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_StudentId",
                table: "StudentTask",
                column: "StudentId");
        }
    }
}
