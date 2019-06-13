using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Infrastructure.Data.Migrations
{
    public partial class OwnerIdForToDoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ToDoItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ToDoItems");
        }
    }
}
