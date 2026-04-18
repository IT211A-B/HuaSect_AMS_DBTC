using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStudentTeacherModelsToProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Student_StudentID",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_TeacherID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.RenameColumn(
                name: "TeacherID",
                table: "Course",
                newName: "TeacherProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_TeacherID",
                table: "Course",
                newName: "IX_Course_TeacherProfileID");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Attendance",
                newName: "StudentProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_StudentID",
                table: "Attendance",
                newName: "IX_Attendance_StudentProfileID");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    StudentNumber = table.Column<string>(type: "text", nullable: false),
                    YearLevel = table.Column<int>(type: "integer", nullable: false),
                    CourseID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentProfile_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeacherProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfile_CourseID",
                table: "StudentProfile",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfile_UserId",
                table: "StudentProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfile_UserId",
                table: "TeacherProfile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_StudentProfile_StudentProfileID",
                table: "Attendance",
                column: "StudentProfileID",
                principalTable: "StudentProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_TeacherProfile_TeacherProfileID",
                table: "Course",
                column: "TeacherProfileID",
                principalTable: "TeacherProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_StudentProfile_StudentProfileID",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_TeacherProfile_TeacherProfileID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "StudentProfile");

            migrationBuilder.DropTable(
                name: "TeacherProfile");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TeacherProfileID",
                table: "Course",
                newName: "TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_TeacherProfileID",
                table: "Course",
                newName: "IX_Course_TeacherID");

            migrationBuilder.RenameColumn(
                name: "StudentProfileID",
                table: "Attendance",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_StudentProfileID",
                table: "Attendance",
                newName: "IX_Attendance_StudentID");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    YearLevel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Department = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_CourseID",
                table: "Student",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Student_StudentID",
                table: "Attendance",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_TeacherID",
                table: "Course",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
