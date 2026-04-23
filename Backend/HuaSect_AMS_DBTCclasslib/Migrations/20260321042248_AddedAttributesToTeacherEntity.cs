using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuaSect_AMS_DBTCclasslib.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributesToTeacherEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Teacher");
        }
    }
}
