using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class MinorChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EduPracticeWeekCount",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdPracticeWeekCount",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionWeekCount",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SearchTag",
                table: "BusinessRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EduPracticeWeekCount",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "ProdPracticeWeekCount",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SessionWeekCount",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SearchTag",
                table: "BusinessRoles");
        }
    }
}
