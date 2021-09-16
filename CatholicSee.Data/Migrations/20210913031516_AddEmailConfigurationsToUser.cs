using Microsoft.EntityFrameworkCore.Migrations;

namespace CatholicSee.Data.Migrations
{
    public partial class AddEmailConfigurationsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShouldReceiveNewHourEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldReceiveSubRequestsEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldReceiveNewHourEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShouldReceiveSubRequestsEmail",
                table: "AspNetUsers");
        }
    }
}
