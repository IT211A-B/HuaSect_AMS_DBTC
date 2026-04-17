using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributesToStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearLevel",
                table: "Student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_CourseID",
                table: "Student",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CourseID",
                table: "Student",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CourseID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CourseID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "YearLevel",
                table: "Student");
        }
    }
}
