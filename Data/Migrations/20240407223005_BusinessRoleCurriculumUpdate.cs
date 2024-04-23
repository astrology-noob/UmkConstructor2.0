using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmkConstructor.Data.Migrations
{
    /// <inheritdoc />
    public partial class BusinessRoleCurriculumUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessRoleId",
                table: "Curricula",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessRoleId",
                table: "Curricula");
        }
    }
}
