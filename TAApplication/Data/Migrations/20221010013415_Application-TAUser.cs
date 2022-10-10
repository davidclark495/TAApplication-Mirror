using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAApplication.Data.Migrations
{
    public partial class ApplicationTAUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_ApplicantId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Applications",
                newName: "TAUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                newName: "IX_Applications_TAUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TAUserId",
                table: "Applications",
                column: "TAUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TAUserId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "TAUserId",
                table: "Applications",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_TAUserId",
                table: "Applications",
                newName: "IX_Applications_ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_ApplicantId",
                table: "Applications",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
