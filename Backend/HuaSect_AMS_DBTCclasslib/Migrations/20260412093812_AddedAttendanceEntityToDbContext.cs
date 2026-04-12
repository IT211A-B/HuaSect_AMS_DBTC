using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttendanceEntityToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "Suffix",
                table: "Student",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Schedule",
                table: "Course",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    CourseID = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attendance_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CourseID",
                table: "Attendance",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentID",
                table: "Attendance",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Student",
                newName: "Suffix");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Course",
                newName: "Schedule");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
