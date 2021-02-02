using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParishManager.Data.Migrations
{
    public partial class AddSubstitutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubstitutionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    SubstitutionUserId = table.Column<string>(nullable: true),
                    TimeSlotCommitmentId = table.Column<int>(nullable: false),
                    DateOfSubstitution = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_AspNetUsers_SubstitutionUserId",
                        column: x => x.SubstitutionUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_TimeSlotCommitments_TimeSlotCommitmentId",
                        column: x => x.TimeSlotCommitmentId,
                        principalTable: "TimeSlotCommitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstitutionRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_SubstitutionUserId",
                table: "SubstitutionRequests",
                column: "SubstitutionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_TimeSlotCommitmentId",
                table: "SubstitutionRequests",
                column: "TimeSlotCommitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_UserId",
                table: "SubstitutionRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubstitutionRequests");
        }
    }
}
