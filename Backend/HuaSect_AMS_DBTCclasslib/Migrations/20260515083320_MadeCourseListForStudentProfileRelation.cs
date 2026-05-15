using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class MadeCourseListForStudentProfileRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfile_Course_CourseID",
                table: "StudentProfile");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfile_CourseID",
                table: "StudentProfile");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "StudentProfile");

            migrationBuilder.AddColumn<int>(
                name: "StudentProfileID",
                table: "Course",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_StudentProfileID",
                table: "Course",
                column: "StudentProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_StudentProfile_StudentProfileID",
                table: "Course",
                column: "StudentProfileID",
                principalTable: "StudentProfile",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_StudentProfile_StudentProfileID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_StudentProfileID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "StudentProfileID",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "StudentProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfile_CourseID",
                table: "StudentProfile",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfile_Course_CourseID",
                table: "StudentProfile",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
