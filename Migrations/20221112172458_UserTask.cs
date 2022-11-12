using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class UserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tasks_AspNetUsers_UserId",
            //    table: "Tasks");

            //migrationBuilder.DropIndex(
            //    name: "IX_Tasks_UserId",
            //    table: "Tasks");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Tasks");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Title",
            //    table: "Tasks",
            //    type: "nvarchar(250)",
            //    maxLength: 250,
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedTaskId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserCreatedTaskId",
                table: "Tasks",
                column: "UserCreatedTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserCreatedTaskId",
                table: "Tasks",
                column: "UserCreatedTaskId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tasks_AspNetUsers_UserCreatedTaskId",
            //    table: "Tasks");

            //migrationBuilder.DropIndex(
            //    name: "IX_Tasks_UserCreatedTaskId",
            //    table: "Tasks");

            //migrationBuilder.DropColumn(
            //    name: "UserCreatedTaskId",
            //    table: "Tasks");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Title",
            //    table: "Tasks",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(250)",
            //    oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tasks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
