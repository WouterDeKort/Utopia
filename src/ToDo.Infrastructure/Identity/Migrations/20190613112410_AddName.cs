using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Infrastructure.Identity.Migrations
{
	public partial class AddName : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Name",
				table: "AspNetUsers",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Name",
				table: "AspNetUsers");
		}
	}
}
