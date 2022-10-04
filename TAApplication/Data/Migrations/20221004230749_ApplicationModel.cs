using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAApplication.Data.Migrations
{
    public partial class ApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PursuingDegree = table.Column<int>(type: "int", nullable: false),
                    Program = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GPA = table.Column<double>(type: "float", nullable: false),
                    HoursWanted = table.Column<int>(type: "int", nullable: false),
                    EarlyAvailability = table.Column<bool>(type: "bit", nullable: false),
                    SemestersCompletedAtUtah = table.Column<int>(type: "int", nullable: false),
                    PersonalStatement = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: true),
                    TransferSchool = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LinkedInURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ResumeFilename = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                column: "ApplicantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
