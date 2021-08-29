using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatholicSee.Data.Migrations
{
    public partial class AddedTrackingFieldsToAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "SubstitutionRequests",
                newName: "LastModifiedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "UserParishAssociations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserParishAssociations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "UserParishAssociations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserParishAssociations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "TimeSlots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "TimeSlots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "TimeSlots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "TimeSlotCommitments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TimeSlotCommitments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "TimeSlotCommitments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "TimeSlotCommitments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SubstitutionRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "SubstitutionRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Parishes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Parishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Parishes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Parishes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserParishAssociations_CreatedById",
                table: "UserParishAssociations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserParishAssociations_LastModifiedById",
                table: "UserParishAssociations",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_CreatedById",
                table: "TimeSlots",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_LastModifiedById",
                table: "TimeSlots",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotCommitments_CreatedById",
                table: "TimeSlotCommitments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotCommitments_LastModifiedById",
                table: "TimeSlotCommitments",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_CreatedById",
                table: "SubstitutionRequests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionRequests_LastModifiedById",
                table: "SubstitutionRequests",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_CreatedById",
                table: "Parishes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_LastModifiedById",
                table: "Parishes",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Parishes_AspNetUsers_CreatedById",
                table: "Parishes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parishes_AspNetUsers_LastModifiedById",
                table: "Parishes",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubstitutionRequests_AspNetUsers_CreatedById",
                table: "SubstitutionRequests",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubstitutionRequests_AspNetUsers_LastModifiedById",
                table: "SubstitutionRequests",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotCommitments_AspNetUsers_CreatedById",
                table: "TimeSlotCommitments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotCommitments_AspNetUsers_LastModifiedById",
                table: "TimeSlotCommitments",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_AspNetUsers_CreatedById",
                table: "TimeSlots",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_AspNetUsers_LastModifiedById",
                table: "TimeSlots",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserParishAssociations_AspNetUsers_CreatedById",
                table: "UserParishAssociations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserParishAssociations_AspNetUsers_LastModifiedById",
                table: "UserParishAssociations",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parishes_AspNetUsers_CreatedById",
                table: "Parishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Parishes_AspNetUsers_LastModifiedById",
                table: "Parishes");

            migrationBuilder.DropForeignKey(
                name: "FK_SubstitutionRequests_AspNetUsers_CreatedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SubstitutionRequests_AspNetUsers_LastModifiedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotCommitments_AspNetUsers_CreatedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotCommitments_AspNetUsers_LastModifiedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_AspNetUsers_CreatedById",
                table: "TimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_AspNetUsers_LastModifiedById",
                table: "TimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_UserParishAssociations_AspNetUsers_CreatedById",
                table: "UserParishAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserParishAssociations_AspNetUsers_LastModifiedById",
                table: "UserParishAssociations");

            migrationBuilder.DropIndex(
                name: "IX_UserParishAssociations_CreatedById",
                table: "UserParishAssociations");

            migrationBuilder.DropIndex(
                name: "IX_UserParishAssociations_LastModifiedById",
                table: "UserParishAssociations");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_CreatedById",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_LastModifiedById",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlotCommitments_CreatedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlotCommitments_LastModifiedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropIndex(
                name: "IX_SubstitutionRequests_CreatedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_SubstitutionRequests_LastModifiedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_Parishes_CreatedById",
                table: "Parishes");

            migrationBuilder.DropIndex(
                name: "IX_Parishes_LastModifiedById",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "UserParishAssociations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserParishAssociations");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "UserParishAssociations");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "UserParishAssociations");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TimeSlotCommitments");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "TimeSlotCommitments");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TimeSlotCommitments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "SubstitutionRequests");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Parishes");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "SubstitutionRequests",
                newName: "UpdatedDate");
        }
    }
}
