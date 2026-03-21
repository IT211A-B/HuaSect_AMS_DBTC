using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributesToCourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schedule",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Course",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Units",
                table: "Course",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherID",
                table: "Course",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_TeacherID",
                table: "Course",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_TeacherID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Units",
                table: "Course");
        }
    }
}
