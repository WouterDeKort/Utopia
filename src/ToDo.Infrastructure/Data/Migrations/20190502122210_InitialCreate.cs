using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ToDo.Infrastructure.Data.Migrations
{
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ToDoItems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Title = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Owner = table.Column<string>(nullable: true),
					Avatar = table.Column<string>(nullable: true),
					Hours = table.Column<int>(nullable: true),
					DueDate = table.Column<DateTime>(nullable: false),
					IsDone = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ToDoItems", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ToDoItems");
		}
	}
}
