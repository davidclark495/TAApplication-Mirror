using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAApplication.Data.Migrations
{
    public partial class ChangedEnrollmentRecordusesCoursenavprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "EnrollmentRecords");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "EnrollmentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRecords_CourseID",
                table: "EnrollmentRecords",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRecords_Courses_CourseID",
                table: "EnrollmentRecords",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRecords_Courses_CourseID",
                table: "EnrollmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentRecords_CourseID",
                table: "EnrollmentRecords");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "EnrollmentRecords");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "EnrollmentRecords",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
