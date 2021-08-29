using Microsoft.EntityFrameworkCore.Migrations;

namespace CatholicSee.Data.Migrations
{
    public partial class AddedMinNumOfAdorersAndEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "TimeSlots",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumNumberOfAdorers",
                table: "TimeSlots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "MinimumNumberOfAdorers",
                table: "TimeSlots");
        }
    }
}
