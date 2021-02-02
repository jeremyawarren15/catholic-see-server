using Microsoft.EntityFrameworkCore.Migrations;

namespace ParishManager.Data.Migrations
{
    public partial class AddIsAdminToUserParishAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserParishAssociations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserParishAssociations");
        }
    }
}
